import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleListPageComponent } from './role-list-page.component';

describe('RoleListPageComponent', () => {
  let component: RoleListPageComponent;
  let fixture: ComponentFixture<RoleListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleListPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoleListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
