import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IboxPaymentPageComponent } from './ibox-payment-page.component';

describe('IboxPaymentPageComponent', () => {
  let component: IboxPaymentPageComponent;
  let fixture: ComponentFixture<IboxPaymentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IboxPaymentPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IboxPaymentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
