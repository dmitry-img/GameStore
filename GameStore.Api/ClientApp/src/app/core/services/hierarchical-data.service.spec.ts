import { TestBed } from '@angular/core/testing';

import { HierarchicalDataService } from './hierarchical-data.service';

describe('HierarchicalDataService', () => {
  let service: HierarchicalDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HierarchicalDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
