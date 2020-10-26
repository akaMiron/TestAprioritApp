import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionAddEditComponent } from './position-add-edit.component';

describe('PositionAddEditComponent', () => {
  let component: PositionAddEditComponent;
  let fixture: ComponentFixture<PositionAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
