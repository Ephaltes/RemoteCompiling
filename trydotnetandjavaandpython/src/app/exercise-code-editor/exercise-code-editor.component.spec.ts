import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExerciseCodeEditorComponent } from './exercise-code-editor.component';

describe('ExerciseCodeEditorComponent', () => {
  let component: ExerciseCodeEditorComponent;
  let fixture: ComponentFixture<ExerciseCodeEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExerciseCodeEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExerciseCodeEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
