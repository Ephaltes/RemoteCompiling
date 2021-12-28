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
    row.files = this.convertBEtoFEEntity(row.template);
    this.selectedItem = row;
  }
  backFromCodingApp(status: boolean) {
    this.exerciseSelected = status;
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
          fileType = FileNodeType.c;
          break;
        case 2:
          fileType = FileNodeType.cpp;
          break;
        case 3:
          fileType = FileNodeType.java;
          break;
        case 4:
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
