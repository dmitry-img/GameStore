import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisaPaymentPageComponent } from './visa-payment-page.component';

describe('VisaPaymentPageComponent', () => {
  let component: VisaPaymentPageComponent;
  let fixture: ComponentFixture<VisaPaymentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VisaPaymentPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VisaPaymentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
