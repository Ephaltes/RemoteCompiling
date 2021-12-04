import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DomSanitizer } from '@angular/platform-browser';
import { CodeModel } from '@ngstack/code-editor';
import { FileNode, FileNodeType } from '../file-module/file-node';

@Component({
  selector: 'app-exercise-code-editor',
  templateUrl: './exercise-code-editor.component.html',
  styleUrls: ['./exercise-code-editor.component.scss']
})
export class ExerciseCodeEditorComponent implements OnInit {
  @Input() data: FileNode[] = []
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
  constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
    this.nestedTreeControl = new NestedTreeControl<FileNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();
    this.nestedDataSource.data = this.data;
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
    this.nestedDataSource.data = this.data;
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
}
