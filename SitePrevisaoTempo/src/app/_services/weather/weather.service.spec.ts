/* tslint:disable:no-unused-variable */

import { TestBed, inject, getTestBed } from '@angular/core/testing';

import { WeatherService } from './weather.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('Service: Weather', () => {
  let injector: TestBed;
  let weatherService: WeatherService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [WeatherService]
    });
    injector = getTestBed();
    weatherService = injector.get(WeatherService);
    httpMock = injector.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  describe('#searchCities', () => {
    it('returns an Observable<City[]>', () => {
      const cityName = 'Salvador';
      const dummyCities = [
        { name: 'Salvador', customCode: 12345, country: 'BR' },
      ];

      weatherService.searchCities(cityName).subscribe(city => {
        expect(city.length).toBe(1);
      });

      const req = httpMock.expectOne(`${weatherService.baseUrl}/search/${cityName}`);
      expect(req.request.method).toBe('GET');
      req.flush(dummyCities);
    });
  });

  describe('#getForecast', () => {
    it('returns nothing', () => {
      const customCode = 12345;

      weatherService.getForecast(customCode.toString())
        .subscribe(forecast => {
          expect(forecast).toBeNull();
        });

      const req = httpMock.expectOne(`${weatherService.baseUrl}/forecast/${customCode}`);
      expect(req.request.method).toBe('GET');
      req.flush(null);
    });
  });

});
