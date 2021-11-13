import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileNode } from '../file-module/file-node';

@Component({
  selector: 'app-folder-option-dialog',
  templateUrl: './folder-option-dialog.component.html',
  styleUrls: ['./folder-option-dialog.component.scss']
})
export class FolderOptionDialogComponent implements OnInit {
  newFileForm: FormGroup;
  emittingData: FileNode;
  validData: boolean;
  folders: FileNode[];
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public folderList: FileNode[]) {
    this.validData = false;
    this.emittingData=null;
    this.folders=folderList;
    this.newFileForm = fb.group({
      folder: ['', [Validators.required]],
    })
  }

  ngOnInit(): void {
  }
  submit() {
    if (!this.newFileForm.valid) {
      return;
    }
    const value = this.newFileForm.value;
    this.emittingData=value.folder;
    this.validData = true;
    this.newFileForm.reset();
  }
  get folder() {
    return this.newFileForm.get('folder')!;
  }
}
