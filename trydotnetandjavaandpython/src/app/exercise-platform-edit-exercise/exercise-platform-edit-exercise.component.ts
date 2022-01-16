import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { ExerciseService } from '../service/exercise.service';

@Component({
  selector: 'app-exercise-platform-edit-exercise',
  templateUrl: './exercise-platform-edit-exercise.component.html',
  styleUrls: ['./exercise-platform-edit-exercise.component.scss'],
  providers: [ExerciseService]
})
export class ExercisePlatformEditExerciseComponent implements OnInit {
  editExerciseForm: FormGroup;
  validData: boolean;
  constructor(private fb: FormBuilder, private apiService: ExerciseService, @Inject(MAT_DIALOG_DATA) public exercise: ExerciseNode) {
    this.validData = false;
    this.editExerciseForm = fb.group({
      name: [this.exercise.name, [
        Validators.required,
        Validators.minLength(3),
      ]],
      description: [this.exercise.description],
    });
  }
  ngOnInit(): void {
  }
  submit() {
    const value = this.editExerciseForm.value;
    if (!this.editExerciseForm.valid) {
      return;
    }
    this.exercise.name = value.name;
    this.exercise.description = value.description;
    this.apiService.putExercises(this.exercise).subscribe(() => { this.validData = true; this.editExerciseForm.reset(); });
  }
  get name() {
    return this.editExerciseForm.get('name')!;
  }
  get description() {
    return this.editExerciseForm.get('description')!;
  }
}

