import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-new-file',
  templateUrl: './add-new-file.component.html',
  styleUrls: ['./add-new-file.component.scss']
})
export class AddNewFileComponent implements OnInit {
  newFileForm: FormGroup;
  languages = [
    { name: 'C#', value: 'csharp' },
    { name: 'Java', value: 'java' },
    { name: 'Python', value: 'python' },
    { name: 'C', value: 'c' },
    { name: 'C++', value: 'cpp' },
  ];
  templates = [
    { name: 'Hello World', value: `using System;
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
  constructor(private fb: FormBuilder) {
    this.newFileForm = fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      language: ['', [Validators.required]],
      template: [''],
    });
  }

  ngOnInit(): void {
  }

  submit() {
    if (!this.newFileForm.valid) {
      return;
    }
  }
  get name() {
    return this.newFileForm.get('name')!;
  }
  get language() {
    return this.newFileForm.get('language')!;
  }
  get template() {
    return this.newFileForm.get('template')!;
  }
}
