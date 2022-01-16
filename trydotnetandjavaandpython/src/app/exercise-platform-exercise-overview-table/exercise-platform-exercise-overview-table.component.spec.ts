import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformExerciseOverviewTableComponent } from './exercise-platform-exercise-overview-table.component';

describe('ExercisePlatformExerciseOverviewTableComponent', () => {
  let component: ExercisePlatformExerciseOverviewTableComponent;
  let fixture: ComponentFixture<ExercisePlatformExerciseOverviewTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformExerciseOverviewTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformExerciseOverviewTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
