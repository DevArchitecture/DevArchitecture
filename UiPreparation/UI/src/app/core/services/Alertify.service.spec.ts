/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AlertifyService } from './Alertify.service';

describe('Service: Alertify', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlertifyService]
    });
  });

  it('should ...', inject([AlertifyService], (service: AlertifyService) => {
    expect(service).toBeTruthy();
  }));
});
