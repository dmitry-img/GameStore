import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaginationNavComponent } from './pagination-nav.component';

describe('PaginationNavComponent', () => {
  let component: PaginationNavComponent;
  let fixture: ComponentFixture<PaginationNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaginationNavComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaginationNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
