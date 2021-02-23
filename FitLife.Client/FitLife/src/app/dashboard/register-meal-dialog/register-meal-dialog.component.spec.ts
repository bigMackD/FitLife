import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterMealDialogComponent } from './register-meal-dialog.component';

describe('RegisterMealDialogComponent', () => {
  let component: RegisterMealDialogComponent;
  let fixture: ComponentFixture<RegisterMealDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterMealDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterMealDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
