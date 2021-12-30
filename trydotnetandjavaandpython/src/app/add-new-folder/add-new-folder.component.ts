import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { FileNodeType } from '../file-module/file-node';
import { forbiddenEndingValidator, forbiddenNameValidator } from '../forbidden-name.directive';
import { ExerciseService } from '../service/exercise.service';
import { UserProjectService } from '../service/userproject.service';

@Component({
  selector: 'app-add-new-folder',
  templateUrl: './add-new-folder.component.html',
  styleUrls: ['./add-new-folder.component.scss'],
  providers: [ExerciseService, UserProjectService]
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
  validData: boolean;
  constructor(private fb: FormBuilder, public exerciseService: ExerciseService, public userProjectService: UserProjectService) {
    exerciseService.getExercises().subscribe(res => this.exercises = res.data);
    this.validData = false;
    this.newFileForm = fb.group({
      name: [null,
        [
          Validators.required,
          Validators.minLength(3),
        ],
      ],
      template: [null, Validators.required],

    });
  }

  ngOnInit(): void {
  }

  submit() {
    if (!this.newFileForm.valid) {
      return;
    }
    const value = this.newFileForm.value;
    this.userProjectService.postProject(value.template.id, value.name, value.template.template.projectType, value.template.template.files).subscribe(() => {
      this.validData = true;
      this.newFileForm.reset();
    })

  }
  get name() {
    return this.newFileForm.get('name')!;
  }
  get template() {
    return this.newFileForm.get('template')!;
  }
}
