import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListManagementPageComponent } from './game-list-management-page.component';

describe('GameListManagementPageComponent', () => {
  let component: GameListManagementPageComponent;
  let fixture: ComponentFixture<GameListManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameListManagementPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameListManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
