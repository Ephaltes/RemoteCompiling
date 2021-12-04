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
  }, { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },
  { id: 1, name: "hello world", author: "test author", description: "hello world aufgabe", files: [] },]

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
  backFromCodingApp(status:boolean){
    this.exerciseSelected=status;
  }
}
