import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardClientesComponent } from './dashboard-clientes.component';

describe('DashboardClientesComponent', () => {
  let component: DashboardClientesComponent;
  let fixture: ComponentFixture<DashboardClientesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardClientesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
