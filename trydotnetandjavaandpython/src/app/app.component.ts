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
import { CompileService } from './service/compile.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [FileDatabase, CompileService]
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
  langVersions = { csharp: ["5"], java: ["15.0.2"], python: ["3.9.1", "2.7.18"] }
  selectedCSharpVersion = "";
  selectedJavaVersion = "";
  selectedPythonVersion = "";
  selectedVersion = "";
  selectedModel: CodeModel = null;
  selectedTheme = 'vs-dark';
  readOnly = false;
  isLoading = false;
  isLoading$: Observable<boolean>;
  output = "";

  @ViewChild('file')
  fileInput: ElementRef;

  options = {
    contextmenu: true,
    minimap: {
      enabled: false,
    },
  };
  constructor(database: FileDatabase, editorService: CodeEditorService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private compileService: CompileService) {
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
    this.selectedCSharpVersion = this.langVersions.csharp[0];
    this.selectedJavaVersion = this.langVersions.java[0];
    this.selectedPythonVersion = this.langVersions.python[0];
  }

  hasNestedChild(_: number, nodeData: FileNode): boolean {
    return nodeData.type === FileNodeType.folder;
  }

  private _getChildren = (node: FileNode) => node.children;

  onCodeChanged(value: any) {
    //console.log('CODE', value);
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
  runCode() {
    if (this.selectedModel.language == 'csharp')
      this.selectedVersion = this.selectedCSharpVersion;
    if (this.selectedModel.language == 'java')
      this.selectedVersion = this.selectedJavaVersion;
    if (this.selectedModel.language == 'python')
      this.selectedVersion = this.selectedPythonVersion;
    this.output = "Loading...";
    this.isLoading = true;
    this.compileService.compile(this.selectedVersion, this.selectedModel).subscribe((item => { this.output = item.run.stdout; this.isLoading = false }))

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
