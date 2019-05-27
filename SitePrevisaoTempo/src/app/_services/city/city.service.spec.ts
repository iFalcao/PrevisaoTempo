/* tslint:disable:no-unused-variable */

import { TestBed, getTestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { CityService } from './city.service';


describe('CityService', () => {
  let injector: TestBed;
  let service: CityService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CityService]
    });
    injector = getTestBed();
    service = injector.get(CityService);
    httpMock = injector.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  describe('#getAllCities', () => {
    it('should return an Observable<City[]>', () => {
      const dummyCities = [
        { name: 'Salvador', customCode: 12345, country: 'BR' },
        { name: 'Santos', customCode: 76545, country: 'BR' }
      ];

      service.getAllCities().subscribe(cities => {
        expect(cities.length).toBe(2);
        expect(cities).toEqual(dummyCities);
      });

      const req = httpMock.expectOne(service.baseUrl);
      expect(req.request.method).toBe('GET');
      req.flush(dummyCities);
    });
  });

  describe('#insertCity', () => {
    it('should return an Observable<City> with same city', () => {
      const dummyCity = {
        name: 'Salvador',
        customCode: 12345,
        country: 'BR'
      };

      service.insertCity(dummyCity).subscribe(insertedCity => {
        expect(insertedCity.name).toBe('Salvador');
        expect(insertedCity).toEqual(dummyCity);
      });

      const req = httpMock.expectOne(service.baseUrl);
      expect(req.request.method).toBe('POST');
      req.flush(dummyCity);
    });
  });

});
