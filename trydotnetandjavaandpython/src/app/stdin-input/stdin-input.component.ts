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
  emittingData: string
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public project: any, public userProjectService: UserProjectService) {
    this.emittingData = ""
    this.validData = false;
    this.stdinForm = fb.group({
      stdin: ['']
    });
  }

  ngOnInit(): void {
  }

  submit() {
    if (!this.stdinForm.valid) {
      return;
    }
    const value = this.stdinForm.value;
    this.emittingData = value.stdin;
    this.validData = true;
    this.stdinForm.reset();
  }
  get name() {
    return this.stdinForm.get('stdin')!;
  }
}
