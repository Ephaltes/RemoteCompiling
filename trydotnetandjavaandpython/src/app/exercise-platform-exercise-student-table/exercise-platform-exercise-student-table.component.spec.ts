import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformExerciseStudentTableComponent } from './exercise-platform-exercise-student-table.component';

describe('ExercisePlatformExerciseStudentTableComponent', () => {
  let component: ExercisePlatformExerciseStudentTableComponent;
  let fixture: ComponentFixture<ExercisePlatformExerciseStudentTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformExerciseStudentTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformExerciseStudentTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
