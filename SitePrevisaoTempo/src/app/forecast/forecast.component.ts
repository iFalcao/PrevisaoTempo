import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap, } from '@angular/router';
import { WeatherService } from '../_services/weather/weather.service';
import { switchMap } from 'rxjs/operators';

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
    private weatherService: WeatherService
  ) { }

  ngOnInit() {
    const selectedCustomCode = this.route.snapshot.paramMap.get('customCode');
    this.weatherService.getForecast(selectedCustomCode).subscribe((forecast: any) => {
      this.forecastInfo = forecast;
      console.log(this.forecastInfo);
    });
    // this.forecastInfo$ = this.route.paramMap.pipe(
    //   switchMap((params: ParamMap) =>
    //     this.weatherService.getForecast(params.get('customCode'))
    //   )
    // );
  }

}
