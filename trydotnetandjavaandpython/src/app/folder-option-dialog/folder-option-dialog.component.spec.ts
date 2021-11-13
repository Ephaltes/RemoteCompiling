import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolderOptionDialogComponent } from './folder-option-dialog.component';

describe('FolderOptionDialogComponent', () => {
  let component: FolderOptionDialogComponent;
  let fixture: ComponentFixture<FolderOptionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FolderOptionDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FolderOptionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
