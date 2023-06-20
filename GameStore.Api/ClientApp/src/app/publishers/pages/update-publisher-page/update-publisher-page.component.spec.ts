import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatePublisherPageComponent } from './update-publisher-page.component';

describe('UpdatePublisherPageComponent', () => {
  let component: UpdatePublisherPageComponent;
  let fixture: ComponentFixture<UpdatePublisherPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdatePublisherPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatePublisherPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
