import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformCorrectComponent } from './exercise-platform-correct.component';

describe('ExercisePlatformCorrectComponent', () => {
  let component: ExercisePlatformCorrectComponent;
  let fixture: ComponentFixture<ExercisePlatformCorrectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformCorrectComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformCorrectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
