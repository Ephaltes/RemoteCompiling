import { Component, OnInit } from '@angular/core';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { StudentNode } from '../exercise-module/student-node';

@Component({
  selector: 'app-exercise-platform-correct',
  templateUrl: './exercise-platform-correct.component.html',
  styleUrls: ['./exercise-platform-correct.component.scss']
})

export class ExercisePlatformCorrectComponent implements OnInit {
  selectedItem: ExerciseNode = { id: 1, name: "", author: "", description: "", files: [], students: [] };
  exerciseSelected = false;
  studentSelected = false;
  selectedStudent: StudentNode = { id: "ifxxbxxx", name: "", grading: 0, files: [] }
  lengthOfStudentList = 0;
  currentStudentIndex = 0;
  constructor() { }

  ngOnInit(): void {
  }
  openStudentList(row: ExerciseNode) {
    this.exerciseSelected = true;
    this.selectedItem = row;
  }
  openCodingApp(row: StudentNode) {
    this.studentSelected = true;
    this.selectedStudent = row;
    this.lengthOfStudentList = this.selectedItem.students.length;
    this.currentStudentIndex = this.selectedItem.students.indexOf(this.selectedStudent) + 1;
  }
  backFromCodingApp(status: boolean) {
    this.studentSelected = status
  }
  backFromStudentList(status: boolean) {
    this.exerciseSelected = status
  }
  changeToPreviousStudent() {
    if (this.selectedItem != null && this.selectedStudent != null) {
      var previousIndex = this.selectedItem.students.indexOf(this.selectedStudent) - 1;
      if (previousIndex >= 0) {
        this.selectedStudent = this.selectedItem.students[previousIndex];
        this.currentStudentIndex = previousIndex + 1;
      }
    }
  }
  changeToNextStudent() {
    if (this.selectedItem != null && this.selectedStudent != null) {
      var nextIndex = this.selectedItem.students.indexOf(this.selectedStudent) + 1;
      if (nextIndex != this.selectedItem.students.length) {
        this.selectedStudent = this.selectedItem.students[nextIndex];
        this.currentStudentIndex = nextIndex + 1;
      }
    }
  }
}
