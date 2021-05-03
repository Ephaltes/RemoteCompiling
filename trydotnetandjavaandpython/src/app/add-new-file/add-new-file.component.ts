import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileNode, FileNodeType } from '../file-node';

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
  emittingData: { name: string, language: FileNodeType, code: string }
  validData:boolean;
  constructor(private fb: FormBuilder) {
    this.validData=false;
    this.emittingData = { name: "", language: FileNodeType.csharp, code: "" };
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
    const value = this.newFileForm.value;
    if (value.language == "csharp") {
      this.emittingData.language = FileNodeType.csharp;
      this.emittingData.name = value.name+".cs";
    }
    if (value.language == "java") {
      this.emittingData.language = FileNodeType.java;
      this.emittingData.name = value.name+".java";
    }
    if (value.language == "python") {
      this.emittingData.language = FileNodeType.python;
      this.emittingData.name = value.name+".py";
    }
    if (value.language == "c") {
      this.emittingData.language = FileNodeType.c;
      this.emittingData.name = value.name+".c";
    }
    if (value.language == "cpp") {
      this.emittingData.language = FileNodeType.cpp;
      this.emittingData.name = value.name+".cpp";
    }
    this.emittingData.code = value.template;
    this.validData=true;
    this.newFileForm.reset();

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
