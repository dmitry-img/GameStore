import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankPaymentPageComponent } from './bank-payment-page.component';

describe('BankPaymentPageComponent', () => {
  let component: BankPaymentPageComponent;
  let fixture: ComponentFixture<BankPaymentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankPaymentPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankPaymentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
