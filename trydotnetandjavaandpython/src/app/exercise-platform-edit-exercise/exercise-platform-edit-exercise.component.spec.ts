import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformEditExerciseComponent } from './exercise-platform-edit-exercise.component';

describe('ExercisePlatformEditExerciseComponent', () => {
  let component: ExercisePlatformEditExerciseComponent;
  let fixture: ComponentFixture<ExercisePlatformEditExerciseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformEditExerciseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformEditExerciseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
