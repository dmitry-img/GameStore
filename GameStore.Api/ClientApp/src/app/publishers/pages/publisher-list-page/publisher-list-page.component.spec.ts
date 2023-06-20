import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherListPageComponent } from './publisher-list-page.component';

describe('PublisherListPageComponent', () => {
  let component: PublisherListPageComponent;
  let fixture: ComponentFixture<PublisherListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublisherListPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PublisherListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
