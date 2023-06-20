import { TestBed } from '@angular/core/testing';

import { IsGameAssociatedWithPublisherGuard } from './is-game-associated-with-publisher.guard';

describe('IsGameAssociatedWithPublisherGuard', () => {
  let guard: IsGameAssociatedWithPublisherGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(IsGameAssociatedWithPublisherGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
