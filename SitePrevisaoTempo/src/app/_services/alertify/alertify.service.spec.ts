/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AlertifyService } from './alertify.service';

describe('Service: Alertify', () => {
  let service: AlertifyService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlertifyService]
    });
  });

  it('should create the service', () => {
    expect(service).toBeUndefined();
    service = TestBed.get(AlertifyService);
    expect(service).toBeDefined();
  });

});
