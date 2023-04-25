import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePublisherPageComponent } from './create-publisher-page.component';

describe('CreatePublisherPageComponent', () => {
  let component: CreatePublisherPageComponent;
  let fixture: ComponentFixture<CreatePublisherPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePublisherPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatePublisherPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
