import { TestBed } from '@angular/core/testing';

import { CheckboxListService } from './checkbox-list.service';

describe('CheckboxListService', () => {
  let service: CheckboxListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckboxListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
