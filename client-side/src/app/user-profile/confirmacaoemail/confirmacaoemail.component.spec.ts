import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmacaoemailComponent } from './confirmacaoemail.component';

describe('ConfirmacaoemailComponent', () => {
  let component: ConfirmacaoemailComponent;
  let fixture: ComponentFixture<ConfirmacaoemailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmacaoemailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmacaoemailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
