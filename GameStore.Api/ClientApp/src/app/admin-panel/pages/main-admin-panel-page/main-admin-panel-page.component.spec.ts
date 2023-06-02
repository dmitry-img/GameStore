import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainAdminPanelPageComponent } from './main-admin-panel-page.component';

describe('MainAdminPanelPageComponent', () => {
  let component: MainAdminPanelPageComponent;
  let fixture: ComponentFixture<MainAdminPanelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainAdminPanelPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainAdminPanelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
