import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorComponent, CodeModel } from '@ngstack/code-editor';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { UserProject } from '../service/userproject.service';

@Component({
  selector: 'app-exercise-platform-create',
  templateUrl: './exercise-platform-create.component.html',
  styleUrls: ['./exercise-platform-create.component.scss']
})

export class ExercisePlatformCreateComponent implements OnInit {

  selectedItem: ExerciseNode = { id: 1, name: "", author: "", description: "", files: [] };
  exerciseSelected = false;

  constructor() { }

  ngOnInit(): void {
  }

  openCodingApp(row: ExerciseNode) {
    this.exerciseSelected = true;
    this.selectedItem = row;
  }
  backFromCodingApp(status: boolean) {
    this.exerciseSelected = status;
  }
}
