import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwitterUpdatesComponent } from './twitter-updates.component';

describe('TwitterUpdatesComponent', () => {
  let component: TwitterUpdatesComponent;
  let fixture: ComponentFixture<TwitterUpdatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TwitterUpdatesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TwitterUpdatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
