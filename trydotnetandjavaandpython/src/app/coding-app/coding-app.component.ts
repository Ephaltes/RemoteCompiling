import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorService, CodeModel } from '@ngstack/code-editor';
import { interval, Observable, Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { FileDatabase } from '../file-module/file-database';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { DomSanitizer } from "@angular/platform-browser";
import { CompileService } from '../service/compile.service';
import { FileCode } from '../file-module/file-code';
import { MatDialog } from '@angular/material/dialog';
import { AddNewFileComponent } from '../add-new-file/add-new-file.component';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';
import { Toast, ToasterService } from 'angular2-toaster';
import { languages } from '../code-meta/codeLanguageMeta';
import * as JSZip from 'jszip';
import { FolderOptionDialogComponent } from '../folder-option-dialog/folder-option-dialog.component';
import { AddNewFolderComponent } from '../add-new-folder/add-new-folder.component';
import { UserProjectService } from '../service/userproject.service';

@Component({
  selector: 'app-coding-app',
  templateUrl: './coding-app.component.html',
  styleUrls: ['./coding-app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [FileDatabase, CompileService, UserProjectService]
})
export class CodingAppComponent implements OnInit, OnDestroy {
  private toasterSerivce: ToasterService;
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

  constructor(private database: FileDatabase, private userProjectService: UserProjectService, private editorService: CodeEditorService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private compileService: CompileService, private Dialog: MatDialog, private ToasterService: ToasterService) {
    this.nestedTreeControl = new NestedTreeControl<FileNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();

    this.refreshData();

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
    this.runFiles = [];
    this.fileUploadControl.acceptFiles(".cs/,.java/,.py/,.c/,.cpp/")
    this.fileUploadControl.valueChanges.subscribe(item => this.dragDropToList(item[item.length - 1]));
    this.toasterSerivce = ToasterService;
  }
  refreshData() {
    var projects: FileNode[];
    this.userProjectService.getProjects().subscribe(res => {
      projects = this.userProjectService.convertBEtoFEEntity(res);
      this.nestedDataSource.data = projects;
      if (projects.length > 0) {
        this.nestedTreeControl.expand(this.nestedDataSource.data[0]);
        if (projects[0].children.length > 0)
          this.selectNode(this.nestedDataSource.data[0].children[0]);
      }
    });
  }
  refreshTree() {
    let _data = this.nestedDataSource.data;
    this.nestedDataSource.data = null;
    this.nestedDataSource.data = _data;
  }
  dragDropToList(file: File) {
    if (file != undefined) {
      const dialogRef = this.Dialog.open(FolderOptionDialogComponent, { data: this.database.getAllFolders() });
      dialogRef.afterClosed().subscribe(result => {
        var folderName = dialogRef.componentInstance.emittingData;
        var folderList = this.database.fileNamesForFoldersWithExt(folderName);
        var fileReader = new FileReader();
        fileReader.readAsText(file);
        fileReader.onload = () => {
          if (folderList.includes(file.name.toLowerCase())) {
            this.fileUploadControl.removeFile(file);
            this.Dialog.open(ErrorDialogComponent, { data: { message: "Found duplicate filenames while uploading files!" } })
            return;
          }
          var fileType = null
          if (file.name.endsWith(".cs")) {
            fileType = FileNodeType.csharp
          }
          if (file.name.endsWith(".java")) {
            fileType = FileNodeType.java
          }
          if (file.name.endsWith(".py")) {
            fileType = FileNodeType.python
          }
          if (file.name.endsWith(".c")) {
            fileType = FileNodeType.c
          }
          if (file.name.endsWith(".cpp")) {
            fileType = FileNodeType.cpp
          }

          if (fileType == null) {
            this.Dialog.open(ErrorDialogComponent, { data: { message: "File extension is not supported, please only use .cs,.java,.py,.c,.cpp files!" } })
            this.fileUploadControl.removeFile(file);
          }
          this.database.addFile(folderName, file.name, fileType, fileReader.result.toString());
          this.fileUploadControl.removeFile(file);
          this.refreshTree();
          return;
        }
        this.fileUploadControl.removeFile(file);
      });
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

    zip.generateAsync({ type: "blob" }).then(function (content) {
      const current = new Date();
      const dataURL = window.URL.createObjectURL(content);
      const link = document.createElement('a');
      link.href = dataURL;
      link.download = "CodeFiles-" + current.getTime().toString();
      link.click();

      setTimeout(() => {
        // For Firefox it is necessary to delay revoking the ObjectURL
        window.URL.revokeObjectURL(dataURL);
      }, 100);
    })
  }
  removeNode(node: FileNode) {
    if (this.isNodeSelected(node)) {
      this.selectedModel = { uri: "", language: "", value: "" };
    }
    this.userProjectService.deleteProject(node.projectid).subscribe(() => {
      this.refreshData()
      this.refreshTree();
    })
  }
  onEditorLoaded() {
    console.log('Online Editor loaded!');
  }
  runCode() {
    this.runFiles = [];
    this.isLoading = true;
    this.output = "Loading...";
    this.compileService.compile(this.selectedModel, this.runFiles).subscribe((item => { this.isLoading = false; item.data.run.stderr.length > 0 ? this.output = item.data.run.stderr : this.output = item.data.run.stdout }))

  }
  openNewFolderDialog() {
    const dialogRef = this.Dialog.open(AddNewFolderComponent, { data: this.database.fileNames() });
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.refreshData() : false })
  }
  openNewFileDialog(node: FileNode) {
    const dialogRef = this.Dialog.open(AddNewFileComponent);
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.addNewFile(node.name, dialogRef.componentInstance.emittingData) : false })
  }
  addNewFolder(data: any) {
    this.database.addFolder(data.name);
    this.refreshTree();
  }
  addNewFile(folderName: string, data: any) {
    this.database.addFile(folderName, data.name, data.language, data.code);
    this.refreshTree();
  }

  ngOnInit() {
    var toast: Toast = {
      type: 'success',
      title: 'Auto save complete',
      showCloseButton: false
    };
    this.saveSource.subscribe(val => { this.database.save(), this.toasterSerivce.pop(toast) });
    //this.nestedTreeControl.expand(this.nestedDataSource.data[0]);
    // this.selectNode(this.nestedDataSource.data[0].children[0]);
  }
  ngOnDestroy() {
    this.database.save();
  }

}
