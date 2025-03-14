import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesClientesComponent } from './detalhes-clientes.component';

describe('DetalhesClientesComponent', () => {
  let component: DetalhesClientesComponent;
  let fixture: ComponentFixture<DetalhesClientesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalhesClientesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalhesClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
