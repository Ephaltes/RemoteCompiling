import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformCreateComponent } from './exercise-platform-create.component';

describe('ExercisePlatformCreateComponent', () => {
  let component: ExercisePlatformCreateComponent;
  let fixture: ComponentFixture<ExercisePlatformCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
