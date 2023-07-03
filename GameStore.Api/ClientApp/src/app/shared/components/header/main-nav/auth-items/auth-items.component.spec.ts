import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthItemsComponent } from './auth-items.component';

describe('AuthItemsComponent', () => {
  let component: AuthItemsComponent;
  let fixture: ComponentFixture<AuthItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthItemsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
