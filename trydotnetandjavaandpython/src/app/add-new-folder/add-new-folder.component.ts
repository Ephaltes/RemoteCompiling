import { AfterViewInit, Component, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelect } from '@angular/material/select';
import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
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

export class AddNewFolderComponent implements OnInit, OnDestroy {
  @ViewChild('singleSelect', { static: true }) singleSelect: MatSelect;
  newFileForm: FormGroup;
  protected exercises: ExerciseNode[] = []
  public filteredExercises: ReplaySubject<ExerciseNode[]> = new ReplaySubject<ExerciseNode[]>(1);
  protected _onDestroy = new Subject<void>();
  validData: boolean;
  constructor(private fb: FormBuilder, public exerciseService: ExerciseService, public userProjectService: UserProjectService) {
    exerciseService.getExercises().subscribe(res => {
      this.exercises = res.data
      this.filterExercises()
    });
    this.validData = false;
    this.newFileForm = fb.group({
      name: [null,
        [
          Validators.required,
          Validators.minLength(3),
        ],
      ],
      template: [null, Validators.required],
      templateFilterCtrl: ['']
    });
  }

  ngOnInit(): void {
    this.filteredExercises.next(this.exercises.slice());
    this.newFileForm.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filterExercises();
      });

  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
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
  protected filterExercises() {
    if (!this.exercises) {
      return;
    }
    // get the search keyword
    let search = this.newFileForm.value.templateFilterCtrl;
    if (!search) {
      this.filteredExercises.next(this.exercises.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the exercises
    this.filteredExercises.next(
      this.exercises.filter(
        exercise => exercise.name.toLowerCase().indexOf(search) > -1 || exercise.author.toLowerCase().indexOf(search) > -1 || exercise.description.toLowerCase().indexOf(search) > -1)
    );
  }
}
