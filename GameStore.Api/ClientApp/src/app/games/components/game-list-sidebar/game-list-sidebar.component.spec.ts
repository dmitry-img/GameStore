import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListSidebarComponent } from './game-list-sidebar.component';

describe('GameListSidebarComponent', () => {
  let component: GameListSidebarComponent;
  let fixture: ComponentFixture<GameListSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameListSidebarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameListSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
