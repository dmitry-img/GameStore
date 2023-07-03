import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FromTheBlogComponent } from './from-the-blog.component';

describe('FromTheBlogComponent', () => {
  let component: FromTheBlogComponent;
  let fixture: ComponentFixture<FromTheBlogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FromTheBlogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FromTheBlogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
