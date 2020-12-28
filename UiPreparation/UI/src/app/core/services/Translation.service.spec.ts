/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TranslationService } from './Translation.service';

describe('Service: Translation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TranslationService]
    });
  });

  it('should ...', inject([TranslationService], (service: TranslationService) => {
    expect(service).toBeTruthy();
  }));
});
