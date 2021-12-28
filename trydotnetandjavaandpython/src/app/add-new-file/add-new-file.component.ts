import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { forbiddenEndingValidator, forbiddenNameValidator } from '../forbidden-name.directive';
import { UserProjectService } from '../service/userproject.service';

@Component({
  selector: 'app-add-new-file',
  templateUrl: './add-new-file.component.html',
  styleUrls: ['./add-new-file.component.scss'],
  providers: [UserProjectService]
})

export class AddNewFileComponent implements OnInit {
  newFileForm: FormGroup;
  validData: boolean;
  emittingData: { projectId: number, projectFileId: number }
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public project: any, public userProjectService: UserProjectService) {
    this.emittingData =  { projectId: this.project.projectid, projectFileId: 0 }
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
    switch (this.project.projectType) {
      case 0:
        fileNameEnding = ".cs"
        break;
      case 1:
        fileNameEnding = ".c"
        break;
      case 2:
        fileNameEnding = ".java"
        break;
      case 3:
        break;
      case 4:
        break;
      case 5:
        fileNameEnding = ".py"
        break;
      default:
        fileNameEnding = ".cs"
        break;
    }

    this.userProjectService.postFileToProject(this.project.projectid, value.name + fileNameEnding).subscribe(res => {
      this.emittingData.projectFileId = res.data;
      this.validData = true;
      this.newFileForm.reset();
    })
  }
  get name() {
    return this.newFileForm.get('name')!;
  }
}
