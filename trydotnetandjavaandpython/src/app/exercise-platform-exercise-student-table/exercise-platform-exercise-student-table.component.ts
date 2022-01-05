import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { HandInNode } from '../exercise-module/handin-node';
import { StudentNode } from '../exercise-module/student-node';
import { FileNode, FileNodeType } from '../file-module/file-node';


@Component({
  selector: 'app-exercise-platform-exercise-student-table',
  templateUrl: './exercise-platform-exercise-student-table.component.html',
  styleUrls: ['./exercise-platform-exercise-student-table.component.scss']
})

export class ExercisePlatformExerciseStudentTableComponent implements OnInit {
  private _currentStudentList: HandInNode[] = []
  @Input() set currentStudentList(value: HandInNode[]) {
    this._currentStudentList = value;
    this.dataSource = new MatTableDataSource<HandInNode>(value)
  }
  get currentStudentList(): HandInNode[] {
    return this._currentStudentList;
  }
  @Output() itemSelectedEvent = new EventEmitter<HandInNode>();
  @Output() finishedStudentListEvent = new EventEmitter<boolean>();
  displayedColumns: string[] = ['id', 'name', 'grading'];
  dataSource = new MatTableDataSource<HandInNode>(this._currentStudentList);
  exerciseSelected = false;
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
  openCodingApp(row: HandInNode) {
    this.itemSelectedEvent.emit(row);
  }
  backToParent() {
    this.finishedStudentListEvent.emit(false);
  }
}
