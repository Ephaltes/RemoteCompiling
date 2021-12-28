import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DomSanitizer } from '@angular/platform-browser';
import { CodeModel } from '@ngstack/code-editor';
import { debounceTime } from 'rxjs/operators';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { StudentNode } from '../exercise-module/student-node';
import { ExercisePlatformCreateFileComponent } from '../exercise-platform-create-file/exercise-platform-create-file.component';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { ExerciseService } from '../service/exercise.service';
import { UserProject } from '../service/userproject.service';

@Component({
  selector: 'app-exercise-code-editor',
  templateUrl: './exercise-code-editor.component.html',
  styleUrls: ['./exercise-code-editor.component.scss'],
  providers: [ExerciseService]
})
export class ExerciseCodeEditorComponent implements OnInit {
  private _currentEditor: FileNode[]
  @Input() set currentEditor(value: FileNode[]) {
    this._currentEditor = value;
    this.nestedDataSource.data = value;
    if (value != undefined) {
      if (value.length > 0) {
        this.selectNode(this.nestedDataSource.data[0])
      }
    } else {
      this.selectedModel = { uri: "", value: "", language: "" }
    }
  }
  get currentEditor(): FileNode[] {
    return this._currentEditor;
  }
  private _currentExercise: ExerciseNode
  @Input() set currentExercise(value: ExerciseNode) {
    this._currentExercise = value;
  }
  get currentExercise(): ExerciseNode {
    return this._currentExercise;
  }
  @Output() finishedWorkingEvent = new EventEmitter<boolean>();
  selectedModel: CodeModel = null;
  selectedTheme = 'vs-dark';
  options = {
    lineNumbers: true,
    contextmenu: true,
    minimap: {
      enabled: false,
    },
  };
  nestedTreeControl: NestedTreeControl<FileNode>;
  nestedDataSource: MatTreeNestedDataSource<FileNode>;
  constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private Dialog: MatDialog, private exerciseService: ExerciseService) {
    this.nestedTreeControl = new NestedTreeControl<FileNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();
    this.nestedDataSource.data = this._currentEditor;
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
  }

  ngOnInit(): void {
  }
  ngDoCheck(): void {

  }
  ngOnChange(changes: SimpleChanges) {
    if (this.nestedDataSource.data != undefined)
      if (this.nestedDataSource.data.length > 0)
        this.selectNode(this.nestedDataSource.data[0])
  }
  removeNode(node: FileNode) {
  }
  private _getChildren = (node: FileNode) => node.children;
  hasNestedChild(_: number, nodeData: FileNode): boolean {
    return nodeData.type === FileNodeType.folder;
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
    this.selectedModel = node.code;
  }
  saveExercise() {
    this.finishedWorkingEvent.emit(false);
  }
  backToParent() {
    this.finishedWorkingEvent.emit(false);
  }
  openCreateExerciseFile() {
    const dialogRef = this.Dialog.open(ExercisePlatformCreateFileComponent, { data: this.currentExercise });
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.refreshData() : false })
  }
  refreshData() {
    this.currentEditor = this.convertBEtoFEEntity(this.currentExercise.template);
  }
  public convertBEtoFEEntity(template: UserProject): FileNode[] {
    var projectsConverted: FileNode[] = [];
    if (template != undefined) {
      var fileType: FileNodeType;
      switch (template.projectType) {
        case 0:
          fileType = FileNodeType.csharp;
          break;
        case 1:
          fileType = FileNodeType.cpp;
          break;
        case 2:
          fileType = FileNodeType.java;
          break;
        case 3:
          fileType = FileNodeType.folder;
          break;
        case 4:
          fileType = FileNodeType.folder;
          break;
        case 5:
          fileType = FileNodeType.python;
          break;
        default:
          fileType = FileNodeType.csharp;
          break;
      }
      if (template.files.length > 0) {
        template.files.forEach(pjFile => {
          var checkpoint = pjFile.checkpoints.reduce((r, o) => r.created < o.created ? r : o);
          var childFile = new FileNode(pjFile.fileName, fileType, checkpoint.code);
          childFile.fileId = pjFile.id;
          childFile.projectid = template.id;
          projectsConverted.push(childFile);
        });
      }
    }
    return projectsConverted;
  }
}
