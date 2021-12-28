import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { FileNodeType } from '../file-module/file-node';
import { forbiddenEndingValidator, forbiddenNameValidator } from '../forbidden-name.directive';
import { ExerciseService } from '../service/exercise.service';

@Component({
  selector: 'app-add-new-folder',
  templateUrl: './add-new-folder.component.html',
  styleUrls: ['./add-new-folder.component.scss'],
  providers: [ExerciseService]
})

export class AddNewFolderComponent implements OnInit {
  newFileForm: FormGroup;
  templates = [
    {
      name: 'Hello World', value: `using System;
    namespace HelloWorld
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine($"Hello, world from .NET and {Angular.name}!");
            }
        }
    }` },
  ];
  exercises: ExerciseNode[] = []
  emittingData: { name: string, language: FileNodeType }
  validData: boolean;
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public fileList: string[], public exerciseService: ExerciseService) {
    exerciseService.getExercises().subscribe(res => this.exercises = res.data);
    this.validData = false;
    this.emittingData = { name: "", language: FileNodeType.folder };
    this.newFileForm = fb.group({
      name: [null, {
        validators: [
          Validators.required,
          Validators.minLength(3),
          forbiddenEndingValidator()
        ],
        asyncValidators: [
          forbiddenNameValidator(fileList)
        ],
        updateOn: 'change'
      }],
      template: [''],

    });
  }

  ngOnInit(): void {
  }

  submit() {
    if (!this.newFileForm.valid) {
      return;
    }
    const value = this.newFileForm.value;
    this.emittingData.name = value.name;
    // template implement as 'aufgabe'
    this.validData = true;
    this.newFileForm.reset();

  }
  get name() {
    return this.newFileForm.get('name')!;
  }
  get template() {
    return this.newFileForm.get('template')!;
  }
}
