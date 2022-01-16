import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileNodeType } from '../file-module/file-node';
import { ExerciseService } from '../service/exercise.service';

@Component({
  selector: 'app-exercise-platform-add-new-exercise',
  templateUrl: './exercise-platform-add-new-exercise.component.html',
  styleUrls: ['./exercise-platform-add-new-exercise.component.scss'],
  providers: [ExerciseService]
})

export class ExercisePlatformAddNewExerciseComponent implements OnInit {
  languages = [
    { name: 'C#', value: FileNodeType.csharp },
    { name: 'Java', value: FileNodeType.java },
    { name: 'Python', value: FileNodeType.python },
    { name: 'C', value: FileNodeType.c },
    { name: 'C++', value: FileNodeType.cpp },
  ];
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
      language: [FileNodeType.csharp, [Validators.required]]
    });
  }
  ngOnInit(): void {
  }
  submit() {
    const value = this.newExerciseForm.value;
    if (!this.newExerciseForm.valid) {
      return;
    }
    this.apiService.postExercises(value.name, value.description, value.language).subscribe(() => { this.validData = true; this.newExerciseForm.reset(); });
  }
  get name() {
    return this.newExerciseForm.get('name')!;
  }
  get description() {
    return this.newExerciseForm.get('description')!;
  }
  get language() {
    return this.newExerciseForm.get('language')!;
  }
}

