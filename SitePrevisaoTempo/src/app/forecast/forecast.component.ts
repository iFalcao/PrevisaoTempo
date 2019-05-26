import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { WeatherService } from '../_services/weather/weather.service';
import { AlertifyService } from '../_services/alertify/alertify.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.css']
})
export class ForecastComponent implements OnInit {
  forecastInfo: any;
  temperatureChart: any;
  humidityPressureChart: any;
  cityCustomCode: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private weatherService: WeatherService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.cityCustomCode = this.route.snapshot.paramMap.get('customCode');

    this.weatherService.getForecast(this.cityCustomCode).subscribe(
      (forecast: any) => {
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
    let humidity = this.forecastInfo.list.map(res => res.main.humidity);
    let pressure = this.forecastInfo.list.map(res => res.main.pressure);

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
    let temp_max = this.forecastInfo.list.map(res =>
      (res.main.temp_max - 273.15).toFixed(2)
    );
    let temp_min = this.forecastInfo.list.map(res =>
      (res.main.temp_min - 273.15).toFixed(2)
    );

    this.temperatureChart = new Chart('temperatureChart', {
      type: 'line',
      data: {
        labels: this.getDates(),
        datasets: [
          {
            data: temp_max,
            borderColor: '#3cba9f',
            fill: true,
            label: 'Temperatura Máxima (ºC)'
          },
          {
            data: temp_min,
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
        let jsDate = new Date(res * 1000);
        weatherDates.push(this.formatDateToGraph(jsDate));
      });
    return weatherDates;
  }

  formatDateToGraph(jsDate: any): string {
    return (
      jsDate.getDate() +
      '/' +
      (jsDate.getMonth() + 1) +
      ' - ' +
      jsDate.getHours() +
      ':00'
    );
  }
}
