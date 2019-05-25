import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/_models/city';
import { CityService } from 'src/app/_services/city/city.service';
import { AlertifyService } from 'src/app/_services/alertify/alertify.service';

@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.scss']
})
export class CityListComponent implements OnInit {
  cities: City[] = [];

  constructor(private cityService: CityService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.cityService.getAllCities()
      .subscribe((cities: City[]) => {
        this.cities = cities;
      }, error => {
        this.alertify.error(error);
      });

    this.cityService.cityCreated$
      .subscribe((insertedCity: City) => {
        this.cities.push(insertedCity);
      });
  }

}
