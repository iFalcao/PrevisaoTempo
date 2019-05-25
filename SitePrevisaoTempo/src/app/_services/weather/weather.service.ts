import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { City } from 'src/app/_models/city';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  baseUrl = environment.baseUrl + 'cities';
  constructor(private http: HttpClient) { }

  searchCities(cityName: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/search/' + cityName);
  }
}
