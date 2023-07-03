import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertisingSidebarComponent } from './advertising-sidebar.component';

describe('AdvertisingSidebarComponent', () => {
  let component: AdvertisingSidebarComponent;
  let fixture: ComponentFixture<AdvertisingSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvertisingSidebarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdvertisingSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
