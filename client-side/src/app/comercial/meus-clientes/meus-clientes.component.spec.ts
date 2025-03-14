import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeusClientesComponent } from './meus-clientes.component';

describe('MeusClientesComponent', () => {
  let component: MeusClientesComponent;
  let fixture: ComponentFixture<MeusClientesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeusClientesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeusClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
