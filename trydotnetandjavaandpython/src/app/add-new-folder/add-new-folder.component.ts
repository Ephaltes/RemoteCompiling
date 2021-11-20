import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileNodeType } from '../file-module/file-node';
import { forbiddenEndingValidator, forbiddenNameValidator } from '../forbidden-name.directive';

@Component({
  selector: 'app-add-new-folder',
  templateUrl: './add-new-folder.component.html',
  styleUrls: ['./add-new-folder.component.scss']
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
  emittingData: { name: string, language: FileNodeType }
  validData: boolean;
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public fileList: string[]) {
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
