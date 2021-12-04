import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorComponent, CodeModel } from '@ngstack/code-editor';
import { ExerciseNode } from '../exercise-module/exercise-node';
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
  selector: 'app-exercise-platform-create',
  templateUrl: './exercise-platform-create.component.html',
  styleUrls: ['./exercise-platform-create.component.scss']
})

export class ExercisePlatformCreateComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'author', 'description', 'action'];
  dataSource = new MatTableDataSource<ExerciseNode>(TEMP_DATA);
  exerciseSelected = false;
  selectedItem: ExerciseNode = { id: 1, name: "", author: "", description: "", files: [] };
  nestedDataSource: MatTreeNestedDataSource<FileNode>
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor() { }

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
    this.exerciseSelected = true;
    this.selectedItem = row;
  }
  backFromCodingApp(status: boolean) {
    this.exerciseSelected = status;
  }
  editExercise(row: ExerciseNode) {
    console.log(row.id);
  }
  deleteExercise(row: ExerciseNode) {
    console.log(row.id);
  }
}
