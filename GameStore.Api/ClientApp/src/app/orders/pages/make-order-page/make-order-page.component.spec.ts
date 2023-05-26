import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeOrderPageComponent } from './make-order-page.component';

describe('MakeOrderPageComponent', () => {
  let component: MakeOrderPageComponent;
  let fixture: ComponentFixture<MakeOrderPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MakeOrderPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MakeOrderPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
