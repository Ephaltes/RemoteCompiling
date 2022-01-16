import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserProject } from '../service/userproject.service';

@Component({
  selector: 'app-folder-option-dialog',
  templateUrl: './folder-option-dialog.component.html',
  styleUrls: ['./folder-option-dialog.component.scss']
})
export class FolderOptionDialogComponent implements OnInit {
  newFileForm: FormGroup;
  emittingData: UserProject;
  validData: boolean;
  projects: UserProject[];
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public projectList: UserProject[]) {
    this.validData = false;
    this.emittingData = null;
    this.projects = projectList;
    this.newFileForm = fb.group({
      project: [undefined, [Validators.required]],
    })
  }

  ngOnInit(): void {
  }
  submit() {
    if (!this.newFileForm.valid) {
      return;
    }
    const value = this.newFileForm.value;
    this.emittingData = value.project;
    this.validData = true;

  }
  get project() {
    return this.newFileForm.get('project')!;
  }
}
