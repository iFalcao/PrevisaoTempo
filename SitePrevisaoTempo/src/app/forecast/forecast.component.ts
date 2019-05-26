import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap, } from '@angular/router';
import { WeatherService } from '../_services/weather/weather.service';
import { switchMap } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify/alertify.service';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.css']
})
export class ForecastComponent implements OnInit {
  forecastInfo: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private weatherService: WeatherService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    const selectedCustomCode = this.route.snapshot.paramMap.get('customCode');

    this.weatherService.getForecast(selectedCustomCode)
      .subscribe((forecast: any) => {
        this.forecastInfo = forecast;
      }, error => {
        this.alertify.error(error);
      });
  }

}
