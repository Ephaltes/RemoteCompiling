import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorService, CodeModel } from '@ngstack/code-editor';
import { interval, Observable, Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { FileDatabase } from './file-module/file-database';
import { FileNode, FileNodeType } from './file-module/file-node';
import { DomSanitizer } from "@angular/platform-browser";
import { CompileService } from './service/compile.service';
import { FileCode } from './file-module/file-code';
import { MatDialog } from '@angular/material/dialog';
import { AddNewFileComponent } from './add-new-file/add-new-file.component';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';
import { ErrorDialogComponent } from './error-dialog/error-dialog.component';
import { Toast, ToasterService } from 'angular2-toaster';
import { languageAmount, languages } from './code-meta/codeLanguageMeta';
import * as JSZip from 'jszip';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [FileDatabase, CompileService]
})


export class AppComponent implements OnInit, OnDestroy {
  private toasterSerivce: ToasterService;
  subscription: Subscription;
  saveSource = interval(60000);
  newFile = "Test.cs"
  newType = FileNodeType.csharp;
  newCode = "";
  nestedTreeControl: NestedTreeControl<FileNode>;
  nestedDataSource: MatTreeNestedDataSource<FileNode>;
  title = 'trydotnetandjavaandpython';
  public fileUploadControl = new FileUploadControl(null, FileUploadValidators.filesLimit(1));

  themes = [
    { name: 'Visual Studio', value: 'vs' },
    { name: 'Visual Studio Dark', value: 'vs-dark' },
    { name: 'High Contrast Dark', value: 'hc-black' },
  ];
  langVersions = { csharp: ["5.0.201"], java: ["15.0.2"], python: ["3.9.1"], gcc: ["10.2.0"] };
  selectedLanguageVersion: string[] = []
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
  constructor(private database: FileDatabase, private editorService: CodeEditorService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private compileService: CompileService, private Dialog: MatDialog, private ToasterService: ToasterService) {
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
    languages.forEach((language, idx) => {
      this.selectedLanguageVersion[idx] = language.version
    });
    this.selectedCSharpVersion = this.langVersions.csharp[0];
    this.selectedJavaVersion = this.langVersions.java[0];
    this.selectedPythonVersion = this.langVersions.python[0];
    this.selectedCVersion = this.langVersions.gcc[0];
    this.selectedCppVersion = this.langVersions.gcc[0];
    this.runFiles = [];
    this.fileUploadControl.acceptFiles(".cs/,.java/,.py/,.c/,.cpp/")
    this.fileUploadControl.valueChanges.subscribe(item => this.dragDropToList(item[item.length - 1]));
    this.toasterSerivce = ToasterService;
  }
  dragDropToList(file: File) {
    if (file != undefined) {
      var fileReader = new FileReader();
      fileReader.readAsText(file);
      fileReader.onload = () => {
        if (this.database.fileNames().includes(this.database.fileEndingRemover(file.name.toLowerCase()))) {
          this.fileUploadControl.removeFile(file);
          this.Dialog.open(ErrorDialogComponent, { data: { message: "Found duplicate filenames while uploading files!" } })
          return;
        }
        if (file.name.endsWith(".cs")) {
          this.database.add(file.name, FileNodeType.csharp, fileReader.result.toString());
          this.fileUploadControl.removeFile(file);
          return;
        }
        if (file.name.endsWith(".java")) {
          this.database.add(file.name, FileNodeType.java, fileReader.result.toString());
          this.fileUploadControl.removeFile(file);
          return;
        }
        if (file.name.endsWith(".py")) {
          this.database.add(file.name, FileNodeType.python, fileReader.result.toString());
          this.fileUploadControl.removeFile(file);
          return;
        }
        if (file.name.endsWith(".c")) {
          this.database.add(file.name, FileNodeType.c, fileReader.result.toString());
          this.fileUploadControl.removeFile(file);
          return;
        }
        if (file.name.endsWith(".cpp")) {
          this.database.add(file.name, FileNodeType.cpp, fileReader.result.toString());
          this.fileUploadControl.removeFile(file);
          return;
        }
        this.Dialog.open(ErrorDialogComponent, { data: { message: "File extension is not supported, please only use .cs,.java,.py,.c,.cpp files!" } })
        this.fileUploadControl.removeFile(file);
      }
      this.fileUploadControl.removeFile(file);
    }
  }

  showDragZone(event: DragEvent) {
    const element = <HTMLElement>document.getElementsByClassName('dropzone')[0];
    element.style.visibility = "";
    element.style.opacity = "1";
  }
  hideDragZone(event: DragEvent) {
    const element = <HTMLElement>document.getElementsByClassName('dropzone')[0];
    element.style.visibility = "hidden";
    element.style.opacity = "0";
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
  downloadFile(node: FileNode) {
    const blob = new Blob([node.code.value], { type: 'text/plain' });
    const dataURL = window.URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = dataURL;
    link.download = node.name;
    link.click();

    setTimeout(() => {
      // For Firefox it is necessary to delay revoking the ObjectURL
      window.URL.revokeObjectURL(dataURL);
    }, 100);
  }
  downloadAllFiles(node: FileNode) {
    console.log(node)
    var zip = new JSZip();
    for (let i = 0; i < node.children?.length; i++)
      zip.file(node.children[i].name, node.children[i].code.value);

    zip.generateAsync({type:"blob"}).then(function(content){
      const current = new Date();
      const dataURL = window.URL.createObjectURL(content);
      const link = document.createElement('a');
      link.href = dataURL;
      link.download = "CodeFiles-"+current.getTime().toString();
      link.click();
  
      setTimeout(() => {
        // For Firefox it is necessary to delay revoking the ObjectURL
        window.URL.revokeObjectURL(dataURL);
      }, 100);
    })
  }
  removeNode(node: FileNode) {
    this.database.remove(node);
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
      this.runFiles.push(new FileCode(this.selectedModel.uri, this.selectedModel.value));
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
    const dialogRef = this.Dialog.open(AddNewFileComponent, { data: this.database.fileNames() });
    dialogRef.afterClosed().subscribe(result => { dialogRef.componentInstance.validData ? this.addNewFile(dialogRef.componentInstance.emittingData) : false })
  }
  addNewFile(data: any) {
    this.database.add(data.name, data.language, data.code);
  }
  ngOnInit() {
    var toast: Toast = {
      type: 'success',
      title: 'Auto save complete',
      showCloseButton: false
    };
    this.saveSource.subscribe(val => { this.database.save(), this.toasterSerivce.pop(toast) });
    this.nestedTreeControl.expand(this.nestedDataSource.data[0]);
    this.selectNode(this.nestedDataSource.data[0].children[0]);
  }
  ngOnDestroy() {
    this.database.save();
    this.subscription.unsubscribe();
  }

}
