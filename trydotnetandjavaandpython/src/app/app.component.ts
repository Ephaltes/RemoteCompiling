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
import { FileCode } from './file-code';
import { MatDialog } from '@angular/material/dialog';
import { AddNewFileComponent } from './add-new-file/add-new-file.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [FileDatabase, CompileService]
})

export class AppComponent implements OnInit {
  newFile = "Test.cs"
  newType = FileNodeType.csharp;
  newCode = "";
  nestedTreeControl: NestedTreeControl<FileNode>;
  nestedDataSource: MatTreeNestedDataSource<FileNode>;
  title = 'trydotnetandjavaandpython';

  themes = [
    { name: 'Visual Studio', value: 'vs' },
    { name: 'Visual Studio Dark', value: 'vs-dark' },
    { name: 'High Contrast Dark', value: 'hc-black' },
  ];
  langVersions = { csharp: ["5.0.201"], java: ["15.0.2"], python: ["3.9.1", "2.7.18"], gcc: ["10.2.0"] };
  selectedCSharpVersion = "";
  selectedJavaVersion = "";
  selectedPythonVersion = "";
  selectedCVersion = "";
  selectedCppVersion = "";
  selectedVersion = "";
  selectedModel: CodeModel = null;
  selectedTheme = 'vs-dark';
  readOnly = false;
  isLoading = false;
  isLoading$: Observable<boolean>;
  runFiles: FileCode[];
  output = "";

  @ViewChild('file')
  fileInput: ElementRef;

  options = {
    lineNumbers: true,
    contextmenu: true,
    minimap: {
      enabled: false,
    },
  };
  constructor(private database: FileDatabase, private editorService: CodeEditorService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private compileService: CompileService, private newFileDialog: MatDialog) {
    this.nestedTreeControl = new NestedTreeControl<FileNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();

    database.dataChange.subscribe(
      (data) => { (this.nestedDataSource.data = data); }
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

    this.matIconRegistry.addSvgIcon(
      `cpp`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`./assets/cpp.svg`)
    );

    this.matIconRegistry.addSvgIcon(
      `c`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`./assets/c.svg`)
    );

    this.selectedCSharpVersion = this.langVersions.csharp[0];
    this.selectedJavaVersion = this.langVersions.java[0];
    this.selectedPythonVersion = this.langVersions.python[0];
    this.selectedCVersion = this.langVersions.gcc[0];
    this.selectedCppVersion = this.langVersions.gcc[0];
    this.runFiles = [];
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
    this.selectedModel = node.code;
  }

  onEditorLoaded() {
    console.log('Online Editor loaded!');
  }
  runCode() {
    this.runFiles = [];
    if (this.selectedModel.language == 'csharp') {
      this.selectedVersion = this.selectedCSharpVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.csharp ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }
    if (this.selectedModel.language == 'java') {
      this.selectedVersion = this.selectedJavaVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.java ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }
    if (this.selectedModel.language == 'python') {
      this.selectedVersion = this.selectedPythonVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.python ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }

    if (this.selectedModel.language == 'cpp') {
      this.selectedVersion = this.selectedCppVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.cpp ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }

    if (this.selectedModel.language == 'c') {
      this.selectedVersion = this.selectedCVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.c ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }

    this.isLoading = true;
    this.output = "Loading...";
    this.compileService.compile(this.selectedVersion, this.selectedModel, this.runFiles).subscribe((item => { this.isLoading = false; item.data.run.stderr.length > 0 ? this.output = item.data.run.stderr : this.output = item.data.run.stdout }))

  }
  openNewFileDialog() {
    const dialogRef = this.newFileDialog.open(AddNewFileComponent,{data:this.database.fileNames()});
    dialogRef.afterClosed().subscribe(result => {dialogRef.componentInstance.validData ? this.addNewFile(dialogRef.componentInstance.emittingData): false})
  }
  addNewFile(data:any) {
    this.database.add(data.name, data.language, data.code);
  }
  ngOnInit() {
    this.selectNode(this.nestedDataSource.data[0]);
    /*
    this.selectedModel = {
      language: 'json',
      uri: 'main.json',
      value: '{}'
    };
    */
  }

}
