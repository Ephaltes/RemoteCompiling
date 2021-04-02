import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorService, CodeModel } from '@ngstack/code-editor';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { FileDatabase } from './file-database';
import { FileNode, FileNodeType } from './file-node';
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [FileDatabase]
})

export class AppComponent implements OnInit {
  nestedTreeControl: NestedTreeControl<FileNode>;
  nestedDataSource: MatTreeNestedDataSource<FileNode>;
  title = 'trydotnetandjavaandpython';

  themes = [
    { name: 'Visual Studio', value: 'vs' },
    { name: 'Visual Studio Dark', value: 'vs-dark' },
    { name: 'High Contrast Dark', value: 'hc-black' },
  ];

  selectedModel: CodeModel = null;
  activeTheme = 'vs-dark';
  readOnly = false;
  isLoading = false;
  isLoading$: Observable<boolean>;

  @ViewChild('file')
  fileInput: ElementRef;

  options = {
    contextmenu: true,
    minimap: {
      enabled: false,
    },
  };
  constructor(database: FileDatabase, editorService: CodeEditorService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
    this.nestedTreeControl = new NestedTreeControl<FileNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();

    database.dataChange.subscribe(
      (data) => { (this.nestedDataSource.data = data); this.nestedTreeControl.expand(data[0]); }
    );

    this.isLoading$ = editorService.loadingTypings.pipe(debounceTime(300));
    this.matIconRegistry.addSvgIcon(
      `csharp`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`./assets/csharp.svg`)
    );
    this.matIconRegistry.addSvgIcon(
      `java`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`./assets/java.svg`)
    );

    this.matIconRegistry.addSvgIcon(
      `python`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`./assets/python.svg`)
    );

  }

  hasNestedChild(_: number, nodeData: FileNode): boolean {
    return nodeData.type === FileNodeType.folder;
  }

  private _getChildren = (node: FileNode) => node.children;

  onCodeChanged(value: any) {
    console.log('CODE', value);
  }
  isNodeSelected(node: FileNode): boolean {
    return (
      node &&
      node.code &&
      this.selectedModel &&
      node.code === this.selectedModel
    );
  }

  selectNode(node: FileNode) {
    this.isLoading = false;
    console.log(node);
    this.selectedModel = node.code;
  }

  onEditorLoaded() {
    console.log('loaded');
  }
  ngOnInit() {
    this.selectNode(this.nestedDataSource.data[0].children[0]);
    /*
    this.selectedModel = {
      language: 'json',
      uri: 'main.json',
      value: '{}'
    };
    */
  }

}
