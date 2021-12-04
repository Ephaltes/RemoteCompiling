import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorComponent, CodeModel } from '@ngstack/code-editor';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { ExercisePlatformAddNewExerciseComponent } from '../exercise-platform-add-new-exercise/exercise-platform-add-new-exercise.component';
import { FileNode, FileNodeType } from '../file-module/file-node';

const TEMP_DATA: ExerciseNode[] = [
  {
    id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [{
      name: 'HelloWorldCSharp.cs',
      type: FileNodeType.csharp,
      code: {
        language: 'csharp',
        uri: 'HelloWorldCSharp.cs',
        value: [
          'using System;',
          `namespace HelloWorld`,
          '{',
          '    class Program',
          '    {',
          `        static void Main(string[] args)`,
          `        {`,
          `            Console.WriteLine($"Hello, world from .NET and {Angular.name}!");`,
          '        }',
          '    }',
          '}'
        ].join('\r\n')
      }
    },
    {
      name: 'HelloAngular.cs',
      type: FileNodeType.csharp,
      code: {
        language: 'csharp',
        uri: 'HelloAngular.cs',
        value: [
          'using System;',
          `namespace HelloWorld`,
          '{',
          '    public class Angular',
          '    {',
          `        public static string name="Angular";`,
          '    }',
          '}'
        ].join('\r\n')
      }
    }]
  }, { id: 2, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 3, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 4, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 5, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 6, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 7, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 8, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 9, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 10, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 11, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 12, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 13, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 14, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 15, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 16, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 17, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 18, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 19, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },]

  @Component({
    selector: 'app-exercise-platform-exercise-overview-table',
    templateUrl: './exercise-platform-exercise-overview-table.component.html',
    styleUrls: ['./exercise-platform-exercise-overview-table.component.scss']
  })

export class ExercisePlatformExerciseOverviewTableComponent implements OnInit {
  @Output() itemSelectedEvent = new EventEmitter<ExerciseNode>();
  displayedColumns: string[] = ['id', 'name', 'author', 'description', 'action'];
  dataSource = new MatTableDataSource<ExerciseNode>(TEMP_DATA);
  exerciseSelected = false;
  nestedDataSource: MatTreeNestedDataSource<FileNode>
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private Dialog: MatDialog) { }

  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  openCodingApp(row: ExerciseNode) {
    this.itemSelectedEvent.emit(row);
  }
  backFromCodingApp(status: boolean) {
    this.exerciseSelected = status;
  }
  createNewExercise(){
    const dialogRef = this.Dialog.open(ExercisePlatformAddNewExerciseComponent);
    dialogRef.afterClosed().subscribe(result => { dialogRef.componentInstance.validData ? true : false }) 
  }
  editExercise(row: ExerciseNode) {
    console.log(row.id);
  }
  deleteExercise(row: ExerciseNode) {
    console.log(row.id);
  }
}
