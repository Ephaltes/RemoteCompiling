import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DomSanitizer } from '@angular/platform-browser';
import { CodeModel } from '@ngstack/code-editor';
import { debounceTime } from 'rxjs/operators';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { HandInNode } from '../exercise-module/handin-node';
import { ExercisePlatformCreateFileComponent } from '../exercise-platform-create-file/exercise-platform-create-file.component';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { ExerciseService } from '../service/exercise.service';
import { convertBEtoFEEntity, convertBEtoFEEntityFromUserProject } from '../service/help.function.service';
import { UserProject } from '../service/userproject.service';

@Component({
  selector: 'app-exercise-code-grading-editor',
  templateUrl: './exercise-code-grading-editor.component.html',
  styleUrls: ['./exercise-code-grading-editor.component.scss'],
  providers: [ExerciseService]
})
export class ExerciseCodeGradingEditorComponent implements OnInit {
  feedback: string = ""
  grading: number = 0;
  private _currentExercise: ExerciseNode;
  @Input() set currentExercise(value: ExerciseNode) {
    this._currentExercise = value;
  }
  get currentExercise(): ExerciseNode {
    return this._currentExercise;
  }
  private _currentEditor: HandInNode;
  @Input() set currentEditor(value: HandInNode) {
    value.files = convertBEtoFEEntityFromUserProject(value.project);
    this._currentEditor = value;
    this.feedback = value.feedback;
    if (value.grade != -1)
      this.grading = value.grade;
    else
      this.grading = 0;
    this.nestedDataSource.data = value.files;
    if (this.nestedDataSource.data.length >= 1)
      this.selectNode(this.nestedDataSource.data[this.nestedDataSource.data.length - 1])
    else
      this.selectedModel = { uri: "", value: "", language: "" }
  }
  get currentEditor(): HandInNode {
    return this._currentEditor;
  }
  @Output() finishedWorkingEvent = new EventEmitter<boolean>();
  @Output() savedWorkingEvent = new EventEmitter<HandInNode>();
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
  saveGrading() {
    this.exerciseService.gradeExercise(this.currentExercise, this.currentEditor, this.feedback, this.grading).subscribe(() => this.savedWorkingEvent.emit( this.currentEditor));
  }
  selectNode(node: FileNode) {
    this.selectedModel = node.code;
  }
  backToParent() {
    this.finishedWorkingEvent.emit(false);
  }
}
