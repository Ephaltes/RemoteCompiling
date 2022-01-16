import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StdinInputComponent } from './stdin-input.component';

describe('StdinInputComponent', () => {
  let component: StdinInputComponent;
  let fixture: ComponentFixture<StdinInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StdinInputComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StdinInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
