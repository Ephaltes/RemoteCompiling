import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisePlatformOverviewComponent } from './exercise-platform-overview.component';

describe('ExercisePlatformOverviewComponent', () => {
  let component: ExercisePlatformOverviewComponent;
  let fixture: ComponentFixture<ExercisePlatformOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisePlatformOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExercisePlatformOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
