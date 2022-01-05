import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { forbiddenEndingValidator, forbiddenNameValidator } from '../forbidden-name.directive';
import { UserProjectService } from '../service/userproject.service';

@Component({
  selector: 'app-stdin-input',
  templateUrl: './stdin-input.component.html',
  styleUrls: ['./stdin-input.component.scss'],
  providers: [UserProjectService]
})

export class StdinInputComponent implements OnInit {
  stdinForm: FormGroup;
  validData: boolean;
  emittingData: { stdin: string, args: string[] }
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public projectStdin: any, public userProjectService: UserProjectService) {
    this.emittingData = { stdin: "", args: [""] }
    this.validData = false;
    this.stdinForm = fb.group({
      stdin: [projectStdin.stdin],
      args: ['']
    });
  }

  ngOnInit(): void {
  }

  submit() {
    if (!this.stdinForm.valid) {
      return;
    }
    const value = this.stdinForm.value;
    this.emittingData.stdin = value.stdin;
    this.emittingData.args = value.args.split(";");
    this.validData = true;
    this.stdinForm.reset();
  }
  get stdin() {
    return this.stdinForm.get('stdin')!;
  }
  get args() {
    return this.stdinForm.get('args')!;
  }
}
