import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/_models/city';
import { CityService } from 'src/app/_services/city/city.service';

@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.scss']
})
export class CityListComponent implements OnInit {
  cities: City[] = [];

  constructor(private cityService: CityService) { }

  ngOnInit() {
    this.cities.push({
      name: 'Salvador',
      customCode: 123456,
      country: 'BR',
      latitude: -45.0053,
      longitude: 55.0053
    });
  }

}
