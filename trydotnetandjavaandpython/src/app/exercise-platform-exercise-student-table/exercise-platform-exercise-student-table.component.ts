import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { StudentNode } from '../exercise-module/student-node';
import { FileNode, FileNodeType } from '../file-module/file-node';


@Component({
  selector: 'app-exercise-platform-exercise-student-table',
  templateUrl: './exercise-platform-exercise-student-table.component.html',
  styleUrls: ['./exercise-platform-exercise-student-table.component.scss']
})

export class ExercisePlatformExerciseStudentTableComponent implements OnInit {
  @Input() data: StudentNode[] = []
  @Output() itemSelectedEvent = new EventEmitter<StudentNode>();
  @Output() finishedStudentListEvent = new EventEmitter<boolean>();
  displayedColumns: string[] = ['id', 'name', 'grading'];
  dataSource = new MatTableDataSource<StudentNode>(this.data);
  exerciseSelected = false;
  nestedDataSource: MatTreeNestedDataSource<FileNode>
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private Dialog: MatDialog) { }

  ngOnInit(): void {
  }
  ngDoCheck(){
    this.dataSource = new MatTableDataSource<StudentNode>(this.data);
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  openCodingApp(row: StudentNode) {
    this.itemSelectedEvent.emit(row);
  }
  backToParent() {
    this.finishedStudentListEvent.emit(false);
  }
}
