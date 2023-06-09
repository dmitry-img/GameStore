import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateGamePageComponent } from './update-game-page.component';

describe('UpdateGamePageComponent', () => {
  let component: UpdateGamePageComponent;
  let fixture: ComponentFixture<UpdateGamePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateGamePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateGamePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
