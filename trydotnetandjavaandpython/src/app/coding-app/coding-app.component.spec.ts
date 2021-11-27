import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodingAppComponent } from './coding-app.component';

describe('CodingAppComponent', () => {
  let component: CodingAppComponent;
  let fixture: ComponentFixture<CodingAppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CodingAppComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CodingAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
