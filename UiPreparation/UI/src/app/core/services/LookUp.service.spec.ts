/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LookUpService } from './LookUp.service';

describe('Service: LookUp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LookUpService]
    });
  });

  it('should ...', inject([LookUpService], (service: LookUpService) => {
    expect(service).toBeTruthy();
  }));
});
