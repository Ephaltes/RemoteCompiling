import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformNavigationComponent } from './exercise-platform-navigation.component';

describe('ExercisePlatformNavigationComponent', () => {
  let component: ExercisePlatformNavigationComponent;
  let fixture: ComponentFixture<ExercisePlatformNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformNavigationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
