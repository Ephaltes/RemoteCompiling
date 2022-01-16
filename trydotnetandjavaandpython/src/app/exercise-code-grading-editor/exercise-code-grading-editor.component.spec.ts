import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExerciseCodeGradingEditorComponent } from './exercise-code-grading-editor.component';

describe('ExerciseCodeGradingEditorComponent', () => {
  let component: ExerciseCodeGradingEditorComponent;
  let fixture: ComponentFixture<ExerciseCodeGradingEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExerciseCodeGradingEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExerciseCodeGradingEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
