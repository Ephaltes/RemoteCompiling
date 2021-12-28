import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExerciseNode } from '../exercise-module/exercise-node';
import { forbiddenEndingValidator } from '../forbidden-name.directive';
import { ExerciseService } from '../service/exercise.service';

@Component({
  selector: 'app-exercise-platform-create-file',
  templateUrl: './exercise-platform-create-file.component.html',
  styleUrls: ['./exercise-platform-create-file.component.scss'],
  providers: [ExerciseService]
})
export class ExercisePlatformCreateFileComponent implements OnInit {
  newFileForm: FormGroup;
  validData: boolean;
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public project: ExerciseNode, public exersiceService: ExerciseService) {
    this.validData = false;
    this.newFileForm = fb.group({
      name: [null, [
        Validators.required,
        Validators.minLength(3),
        forbiddenEndingValidator()
      ],
      ]
    });
  }

  ngOnInit(): void {
  }

  submit() {
    if (!this.newFileForm.valid) {
      return;
    }
    const value = this.newFileForm.value;
    var fileNameEnding = "";
    console.log(this.project)
    switch (this.project.template.projectType) {
      case 0:
        fileNameEnding = ".cs"
        break;
      case 1:
        fileNameEnding = ".c"
        break;
      case 2:
        fileNameEnding = ".c"
        break;
      case 3:
        fileNameEnding = ".java"
        break;
      case 4:
        fileNameEnding = ".py"
        break;
      default:
        fileNameEnding = ".cs"
        break;
    }
    this.project.template.files.push({ fileName: value.name+fileNameEnding, checkpoints: [{ code: "" }] })
    this.exersiceService.putExercises(this.project).subscribe(() => {
      this.validData = true;
      this.newFileForm.reset();
    })
  }
  get name() {
    return this.newFileForm.get('name')!;
  }
}
