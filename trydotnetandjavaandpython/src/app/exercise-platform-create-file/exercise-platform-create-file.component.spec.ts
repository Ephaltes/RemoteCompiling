import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformCreateFileComponent } from './exercise-platform-create-file.component';

describe('ExercisePlatformCreateFileComponent', () => {
  let component: ExercisePlatformCreateFileComponent;
  let fixture: ComponentFixture<ExercisePlatformCreateFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformCreateFileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformCreateFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
