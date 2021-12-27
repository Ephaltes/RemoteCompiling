import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExerciseService } from '../service/exercise.service';

@Component({
  selector: 'app-exercise-platform-add-new-exercise',
  templateUrl: './exercise-platform-add-new-exercise.component.html',
  styleUrls: ['./exercise-platform-add-new-exercise.component.scss'],
  providers: [ExerciseService]
})
export class ExercisePlatformAddNewExerciseComponent implements OnInit {
  newExerciseForm: FormGroup;
  validData: boolean;
  constructor(private fb: FormBuilder, private apiService: ExerciseService) {
    this.validData = false;
    this.newExerciseForm = fb.group({
      name: [null, [
        Validators.required,
        Validators.minLength(3),
      ]],
      description: [''],
    });
  }
  ngOnInit(): void {
  }
  submit() {
    const value = this.newExerciseForm.value;
    if (!this.newExerciseForm.valid) {
      return;
    }
    this.apiService.postExercises(value.name, value.description).subscribe(res => { this.validData = true; this.newExerciseForm.reset(); });
  }
  get name() {
    return this.newExerciseForm.get('name')!;
  }
  get description() {
    return this.newExerciseForm.get('description')!;
  }
}

