import { TestBed } from '@angular/core/testing';

import { IsUserAssociatedWithPublisherGuard } from './is-user-associated-with-publisher.guard';

describe('IsUserAssociatedWithPublisherGuard', () => {
  let guard: IsUserAssociatedWithPublisherGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(IsUserAssociatedWithPublisherGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
