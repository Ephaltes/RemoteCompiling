import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CodeEditorComponent, CodeModel } from '@ngstack/code-editor';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { ExercisePlatformAddNewExerciseComponent } from '../exercise-platform-add-new-exercise/exercise-platform-add-new-exercise.component';
import { ExercisePlatformEditExerciseComponent } from '../exercise-platform-edit-exercise/exercise-platform-edit-exercise.component';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { ExerciseService } from '../service/exercise.service';

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
          `            Console.WriteLine($"Hello, World from .NET and {Angular.name}!");`,
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
    , students: [{
      id: "if20b212", name: "gsingh1", grading: 0, files: [{
        name: 'HelloWorldCSharp1.cs',
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
    }, {
      id: "if20b212", name: "gsingh2", grading: 0, files: [{
        name: 'HelloWorldCSharp2.cs',
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
            `            Console.WriteLine($"Hello2, world from .NET and {Angular.name}!");`,
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
    }, {
      id: "if20b212", name: "gsingh3", grading: 0, files: [{
        name: 'HelloWorldCSharp3.cs',
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
            `            Console.WriteLine($"Hello3, world from .NET and {Angular.name}!");`,
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
    }, {
      id: "if20b212", name: "gsingh4", grading: 0
    }]
  }]


@Component({
  selector: 'app-exercise-platform-exercise-overview-table',
  templateUrl: './exercise-platform-exercise-overview-table.component.html',
  styleUrls: ['./exercise-platform-exercise-overview-table.component.scss'],
  providers: [ExerciseService]
})

export class ExercisePlatformExerciseOverviewTableComponent implements OnInit {
  @Output() itemSelectedEvent = new EventEmitter<ExerciseNode>();
  exercises: ExerciseNode[]
  displayedColumns: string[] = ['id', 'name', 'author', 'description', 'action'];
  dataSource = new MatTableDataSource<ExerciseNode>();
  exerciseSelected = false;
  nestedDataSource: MatTreeNestedDataSource<FileNode>
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private Dialog: MatDialog, private ExerciseService: ExerciseService) { }

  ngOnInit(): void {
    this.refreshData();
  }
  refreshData(): void {
    this.ExerciseService.getExercises().subscribe(res => this.dataSource.data = res.data);
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
  createNewExercise() {
    const dialogRef = this.Dialog.open(ExercisePlatformAddNewExerciseComponent);
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.refreshData() : false; })
  }
  editExercise(row: ExerciseNode) {
    const dialogRef = this.Dialog.open(ExercisePlatformEditExerciseComponent, { data: row });
    dialogRef.afterClosed().subscribe(() => { dialogRef.componentInstance.validData ? this.refreshData() : false })
  }
  deleteExercise(row: ExerciseNode) {
    this.ExerciseService.deleteExercises(row.id).subscribe(res => this.refreshData());
  }
}
