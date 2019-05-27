import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { City } from 'src/app/_models/city';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  baseUrl = environment.baseUrl + 'cities';
  // tslint:disable-next-line: variable-name
  private _cityCreationSource = new Subject<City>();
  cityCreated$ = this._cityCreationSource.asObservable();

  constructor(private http: HttpClient) { }

  getAllCities(): Observable<City[]> {
    return this.http.get<City[]>(this.baseUrl);
  }

  insertCity(newCity: City): Observable<City> {
    return this.http.post<City>(this.baseUrl, newCity);
  }

  noticeCityInsertion(city: City) {
    this._cityCreationSource.next(city);
  }
}
