import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformAddNewExerciseComponent } from './exercise-platform-add-new-exercise.component';

describe('ExercisePlatformAddNewExerciseComponent', () => {
  let component: ExercisePlatformAddNewExerciseComponent;
  let fixture: ComponentFixture<ExercisePlatformAddNewExerciseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformAddNewExerciseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformAddNewExerciseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
