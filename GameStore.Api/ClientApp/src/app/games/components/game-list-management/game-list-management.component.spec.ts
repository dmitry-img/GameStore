import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListManagementComponent } from './game-list-management.component';

describe('GameListManagementComponent', () => {
  let component: GameListManagementComponent;
  let fixture: ComponentFixture<GameListManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameListManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameListManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
