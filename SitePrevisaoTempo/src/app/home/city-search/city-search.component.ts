import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/_models/city';
import { WeatherService } from 'src/app/_services/weather/weather.service';
import { AlertifyService } from 'src/app/_services/alertify/alertify.service';

@Component({
  selector: 'app-city-search',
  templateUrl: './city-search.component.html',
  styleUrls: ['./city-search.component.css']
})
export class CitySearchComponent implements OnInit {
  foundCities: any = [];
  model: any = {};
  pesquisaFeita = false;

  constructor(private weatherService: WeatherService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  searchCities(): void {
    if (this.model.cityName === undefined
      || this.model.cityName.length === 0) {
      this.alertify.error('Por favor, informe o nome da cidade que deseja buscar.');
      return;
    }
    this.weatherService.searchCities(this.model.cityName)
      .subscribe((cities: any) => {
        console.log(cities.list);
        this.foundCities = cities.list;
      }, error => {
        console.log(error);
        this.alertify.error(error);
      }, () => {
        this.pesquisaFeita = true;
      });
  }
}
