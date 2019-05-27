import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { WeatherService } from '../_services/weather/weather.service';
import { AlertifyService } from '../_services/alertify/alertify.service';
import { Chart } from 'chart.js';
import { Forecast, CityForecast } from '../_models/city-forecast';
import { CommonService } from '../_services/common.service';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.css']
})
export class ForecastComponent implements OnInit {
  forecastInfo: CityForecast = { message: '', cod: '', list: null, cnt: 0 };
  temperatureChart: any;
  humidityPressureChart: any;
  cityCustomCode: string;

  constructor(
    private route: ActivatedRoute,
    private weatherService: WeatherService,
    private alertify: AlertifyService,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    this.cityCustomCode = this.route.snapshot.paramMap.get('customCode');

    this.weatherService.getForecast(this.cityCustomCode).subscribe(
      (forecast: CityForecast) => {
        this.forecastInfo = forecast;
        this.createTemperatureChart();
        this.createHumidityPressureChart();
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  createHumidityPressureChart() {
    const humidity = this.forecastInfo.list.map(res => res.main.humidity);
    const pressure = this.forecastInfo.list.map(res => res.main.pressure);

    this.humidityPressureChart = new Chart('humidityPressureChart', {
      type: 'bar',
      data: {
        labels: this.getDates(),
        datasets: [
          {
            data: humidity,
            backgroundColor: '#3cba9f',
            fill: true,
            label: 'Umidade do ar (%)'
          },
          {
            data: pressure,
            backgroundColor: '#ffcc00',
            fill: true,
            label: 'Pressão Atmosférica'
          }
        ]
      },
      options: {
        legend: {
          display: true
        },
        scales: {
          xAxes: [{ display: true }],
          yAxes: [{ display: true }]
        }
      }
    });
  }

  createTemperatureChart() {
    const tempMax = this.forecastInfo.list.map(res =>
      this.commonService
        .tempFromKelvinToCelsiusStr(res.main.temp_max)
    );
    const tempMin = this.forecastInfo.list.map(res =>
      this.commonService
        .tempFromKelvinToCelsiusStr(res.main.temp_min)
    );

    this.temperatureChart = new Chart('temperatureChart', {
      type: 'line',
      data: {
        labels: this.getDates(),
        datasets: [
          {
            data: tempMax,
            borderColor: '#3cba9f',
            fill: true,
            label: 'Temperatura Máxima (ºC)'
          },
          {
            data: tempMin,
            borderColor: '#ffcc00',
            fill: true,
            label: 'Temperatura Mínima (ºC)'
          }
        ]
      },
      options: {
        legend: {
          display: true
        },
        scales: {
          xAxes: [{ display: true }],
          yAxes: [{ display: true }]
        }
      }
    });
  }

  getDates(): string[] {
    let weatherDates = [];
    this.forecastInfo.list
      .map(res => res.dt)
      .forEach((res: number) => {
        weatherDates.push(this.commonService.formatDateFromNumber(res));
      });
    return weatherDates;
  }
}
