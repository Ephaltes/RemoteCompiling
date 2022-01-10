import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorService, CodeModel } from '@ngstack/code-editor';
import { interval, Observable, Subscription } from 'rxjs';
import { catchError, debounceTime } from 'rxjs/operators';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { DomSanitizer } from "@angular/platform-browser";
import { CompileService } from '../service/compile.service';
import { FileCode } from '../file-module/file-code';
import { MatDialog } from '@angular/material/dialog';
import { AddNewFileComponent } from '../add-new-file/add-new-file.component';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';
import { Toast, ToasterService } from 'angular2-toaster';
import * as JSZip from 'jszip';
import { FolderOptionDialogComponent } from '../folder-option-dialog/folder-option-dialog.component';
import { AddNewFolderComponent } from '../add-new-folder/add-new-folder.component';
import { CheckPoint, UserProjectService } from '../service/userproject.service';
import { ExerciseService } from '../service/exercise.service';
import { StdinInputComponent } from '../stdin-input/stdin-input.component';
import { convertBEtoFEEntity, convertFileTypeToNumber } from '../service/help.function.service';
import { error } from 'protractor';
import { Scan, ScanBody, StaticCodeService } from '../service/staticcode.service';

@Component({
  selector: 'app-coding-app',
  templateUrl: './coding-app.component.html',
  styleUrls: ['./coding-app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [CompileService, UserProjectService, ExerciseService, StaticCodeService]
})
export class CodingAppComponent implements OnInit, OnDestroy, AfterViewInit {
  handinButtonDisabled: boolean = false;
  currentCheckpoints: CheckPoint[] = []
  private _selectedCheckpoint: CheckPoint;
  get selectedCheckpoint(): CheckPoint {
    return this._selectedCheckpoint;
  }
  set selectedCheckpoint(value: CheckPoint) {
    if (confirm("Selecting a checkpoint will discard your current changes, do you want to proceed?")) {
      this.nestedDataSource.data.find(c => c.projectid == this.currentProjectId).children
        .find(c => c.fileId = this.currentFileId).code.value = value.code;
      this.selectedModel = { language: this.selectedModel.language, value: value.code, uri: this.selectedModel.uri }
    }
  }
  stdinString: string = ""
  currentldapUid: string = "";
  stdin: boolean = false;
  graded: boolean = false;
  gradedGrade: number = 0;
  gradedFeedback: string = "";
  currentTaskDefinition: string;
  currentExerciseName: string;
  currentExerciseAuthor: string;
  private currentProjectId = 0;
  private currentExerciseId = 0;
  private currentFileId = 0;
  showExerciseNotes = false;
  editorColSpan = 10;
  exerciseColSpan = 0;
  private toasterSerivce: ToasterService;
  private saveSource = interval(60000);
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
  set selectedTheme(value: string) {
    localStorage.setItem("editorTheme", value);
  }
  get selectedTheme(): string {
    var cachedTheme = localStorage.getItem("editorTheme");
    if (cachedTheme == undefined)
      return 'vs-dark';
    else
      return cachedTheme;
  }
  readOnly: boolean = true;
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

  constructor(private staticCodeService: StaticCodeService, private userProjectService: UserProjectService, private exerciseService: ExerciseService, private editorService: CodeEditorService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private compileService: CompileService, private Dialog: MatDialog, private ToasterService: ToasterService) {
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
  ngAfterViewInit(): void {
    // this.toggleExercisNote();
  }
  refreshData() {
    var projects: FileNode[];
    this.userProjectService.getProjects().subscribe(res => {
      projects = convertBEtoFEEntity(res);
      this.nestedDataSource.data = projects;
      if (projects.length > 0) {
        this.nestedTreeControl.expand(this.nestedDataSource.data[0]);
        if (projects[0].children.length > 0)
          this.selectNode(this.nestedDataSource.data[0].children[0]);
      }
      this.currentldapUid = res.data.ldapUid;
    });
  }
  refreshDataNavivagte(projectId: number, projectFileId: number) {
    var projects: FileNode[];
    this.userProjectService.getProjects().subscribe(res => {
      projects = convertBEtoFEEntity(res);
      this.nestedDataSource.data = projects;
      if (projects.length > 0) {
        var nestedTreeProjectValue = this.nestedDataSource.data.find(c => c.projectid == projectId);
        this.nestedTreeControl.expand(this.nestedDataSource.data.find(c => c.projectid == projectId));
        if (nestedTreeProjectValue.children.length > 0)
          this.selectNode(nestedTreeProjectValue.children.find(c => c.fileId == projectFileId));
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
      this.userProjectService.getProjects().subscribe(res => {
        const dialogRef = this.Dialog.open(FolderOptionDialogComponent, { data: res.data.projects });
        dialogRef.afterClosed().subscribe(result => {
          var project = dialogRef.componentInstance.emittingData;
          var fileReader = new FileReader();
          fileReader.readAsText(file);
          fileReader.onload = () => {
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
            } else if (convertFileTypeToNumber(fileType) != project.projectType) {
              this.Dialog.open(ErrorDialogComponent, { data: { message: "File extension does not match project filetype!" } })
              this.fileUploadControl.removeFile(file);
            }
            else {
              this.userProjectService.postFileToProjectWithCode(project.id, file.name, fileReader.result.toString()).subscribe(res => {
                this.fileUploadControl.removeFile(file);
                this.refreshDataNavivagte(project.id, res.data);
                return
              });
            }
          }
          this.fileUploadControl.removeFile(file);
        });
      }
      );
    }
  }
  toggleExercisNote() {
    if (this.showExerciseNotes) {
      this.editorColSpan = 10;
      this.exerciseColSpan = 0;
    } else {
      this.editorColSpan = 7;
      this.exerciseColSpan = 3;
    }
    this.showExerciseNotes = !this.showExerciseNotes;
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
    this.nestedDataSource.data.find(c => c.projectid == this.currentProjectId).children.find(c => c.fileId == this.currentFileId).modified = true;
  }
  isNodeSelected(node: FileNode): boolean {
    return (
      node &&
      node.code &&
      this.selectedModel &&
      node.code === this.selectedModel
    );
  }
  handIn() {
    this.saveButton();
    if (this.currentProjectId > 0 && this.currentExerciseId > 0) {
      this.exerciseService.putExerciseHandIn(this.currentProjectId, this.currentExerciseId).subscribe();
    }
  }
  getGrade() {
    this.exerciseService.getExerciseGrade(this.currentldapUid, this.currentExerciseId).subscribe(res => {
      this.graded = true;
      this.gradedFeedback = res.data.feedback;
      this.gradedGrade = res.data.grade;
    }, err => {
      if (err.status == 404 || err.status == 204)
        this.graded = false;
    })
  }
  getExercisByCurrentId() {
    this.exerciseService.getExercisesById(this.currentExerciseId).subscribe(res => {
      this.currentExerciseAuthor = res.data.author;
      this.currentTaskDefinition = res.data.taskDefinition;
      this.currentExerciseName = res.data.name;
      this.getGrade();
      this.checkIfGraded();
    })
  }
  checkIfGraded() {
    this.exerciseService.getExerciseGradeStatus(this.currentldapUid, this.currentExerciseId).subscribe(res => {
      if (res.data > 0)
        this.handinButtonDisabled = true;
      else
        this.handinButtonDisabled = false;
    }, err => {
      if (err.status == 500 || err.status == 204)
        this.handinButtonDisabled = false;
    })
  }
  selectNode(node: FileNode) {
    if (node.checkpoints != undefined)
      this.currentCheckpoints = node.checkpoints;
    this.stdinString = node.stdin;
    this.isLoading = false;
    this.graded = false;
    this.selectedModel = node.code;
    if (node.exerciseId > 0) {
      this.currentExerciseId = node.exerciseId;
      this.getExercisByCurrentId();
    }
    if (node.projectid > 0) {
      this.currentProjectId = node.projectid;
    }
    if (node.fileId > 0) {
      this.currentFileId = node.fileId;
    }
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
    if (confirm("Are you sure to delete " + node.name + "?")) {
      if (node.fileId == undefined) {
        this.userProjectService.deleteProject(node.projectid).subscribe(() => {
          this.refreshData()
          this.refreshTree();
        })
      }
      else {
        this.userProjectService.deleteFileFromProject(node.projectid, node.fileId).subscribe(() => {
          this.refreshData()
          this.refreshTree();
        })
      }
    }
  }
  onEditorLoaded() {
    console.log('Online Editor loaded!');
  }
  runCode() {
    var stdin = this.nestedDataSource.data.find(c => c.projectid == this.currentProjectId).children
      .find(c => c.fileId = this.currentFileId).stdin;
    var args: string[] = []
    this.runFiles = [];
    if (this.stdin) {
      const dialogRef = this.Dialog.open(StdinInputComponent, { data: { stdin: stdin } });
      dialogRef.afterClosed().subscribe(() => {
        var closedWithoutInput = false;
        args = dialogRef.componentInstance.emittingData.args;
        dialogRef.componentInstance.validData ? stdin = dialogRef.componentInstance.emittingData.stdin : closedWithoutInput = true;
        if (!closedWithoutInput) {
          this.nestedDataSource.data.find(c => c.projectid == this.currentProjectId).children.forEach(file => {
            this.runFiles.push(new FileCode(file.name, file.code.value))
          })
          this.isLoading = true;
          this.output = "Loading...";
          this.compileService.compile(this.selectedModel, this.runFiles, args, stdin).subscribe((item => { this.isLoading = false; item.data.run.stderr.length > 0 ? this.output = item.data.run.stderr : this.output = item.data.run.stdout }))
          this.userProjectService.putProject(this.currentProjectId, stdin).subscribe(() => this.nestedDataSource.data.find(c => c.projectid == this.currentProjectId).children
            .find(c => c.fileId = this.currentFileId).stdin = stdin);
        }
      });
    } else {
      this.nestedDataSource.data.find(c => c.projectid == this.currentProjectId).children.forEach(file => {
        this.runFiles.push(new FileCode(file.name, file.code.value))
      })
      this.isLoading = true;
      this.output = "Loading...";
      this.compileService.compile(this.selectedModel, this.runFiles, args, stdin).subscribe((item => { this.isLoading = false; item.data.run.stderr.length > 0 ? this.output = item.data.run.stderr : this.output = item.data.run.stdout }))
    }
  }
  runStaticCode() {
    var scanbody: Scan = {
      total: 1, issues: [{
        type: 1,
        severity: 1,
        component: "test",
        message: "test",
        textLocation: {
          startLine: 1,
          endLine: 1,
          startOffset: 1,
          endOffset: 1
        }
      }]
    }
    var issueText = "";
    this.isLoading = true;
    this.staticCodeService.postScan(this.selectedModel.value, this.selectedModel.language).subscribe(res => {
      this.staticCodeService.getScanResult(res.id).subscribe(res => { console.log(res) });
    }, err => {
      scanbody.issues.forEach(issues => {
        issueText = issueText + " Errortext: " + issues.message + "\r\n";
      });
      this.output = "Issues: " + scanbody.total + "\r\n" + issueText;
      this.isLoading = false;
    })
  }
  openNewFolderDialog() {
    const dialogRef = this.Dialog.open(AddNewFolderComponent);
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.refreshData() : false })
  }
  openNewFileDialog(node: FileNode) {
    const dialogRef = this.Dialog.open(AddNewFileComponent, { data: { projectid: node.projectid, projectType: node.projectType } });
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.refreshDataNavivagte(dialogRef.componentInstance.emittingData.projectId, dialogRef.componentInstance.emittingData.projectFileId) : false })
  }
  saveButton() {
    this.userProjectService.save(this.nestedDataSource.data);
    this.nestedDataSource.data.forEach(project => {
      project.children.forEach(file => {
        if (file.modified) {
          file.checkpoints.push({ created: new Date(), code: file.code.value })
          file.modified = false;
        }
      })
    });
  }
  ngOnInit() {
    var toast: Toast = {
      type: 'success',
      title: 'Auto save complete',
      showCloseButton: false
    };
    this.saveSource.subscribe(() => { this.saveButton(), this.toasterSerivce.pop(toast) });
  }
  ngOnDestroy() {
    this.saveButton();
  }

}
