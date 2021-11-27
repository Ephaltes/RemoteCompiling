import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformAppComponent } from './exercise-platform-app.component';

describe('ExercisePlatformAppComponent', () => {
  let component: ExercisePlatformAppComponent;
  let fixture: ComponentFixture<ExercisePlatformAppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformAppComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
