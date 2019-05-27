import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { WeatherService } from '../_services/weather/weather.service';
import { AlertifyService } from '../_services/alertify/alertify.service';
import { Chart } from 'chart.js';
import { Forecast, CityForecast } from '../_models/city-forecast';
declare let $: any;

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.css']
})
export class ForecastComponent implements OnInit {
  forecastInfo: CityForecast;
  temperatureChart: any;
  humidityPressureChart: any;
  cityCustomCode: string;

  constructor(
    private route: ActivatedRoute,
    private weatherService: WeatherService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    $(function () {
      'use strict';
      // Auto-scroll
      $('#myCarousel').carousel({
        interval: 5000
      });

      // Control buttons
      $('.next').click(function () {
        $('.carousel').carousel('next');
        return false;
      });
      $('.prev').click(function () {
        $('.carousel').carousel('prev');
        return false;
      });

      // On carousel scroll
      $('#myCarousel').on('slide.bs.carousel', function (e) {
        const $e = $(e.relatedTarget);
        const idx = $e.index();
        const itemsPerSlide = 3;
        const totalItems = $('.carousel-item').length;
        if (idx >= totalItems - (itemsPerSlide - 1)) {
          const it = itemsPerSlide -
            (totalItems - idx);
          for (let i = 0; i < it; i++) {
            // append slides to end
            if (e.direction == 'left') {
              $(
                '.carousel-item').eq(i).appendTo('.carousel-inner');
            } else {
              $('.carousel-item').eq(0).appendTo('.carousel-inner');
            }
          }
        }
      });
    });

    this.cityCustomCode = this.route.snapshot.paramMap.get('customCode');

    this.weatherService.getForecast(this.cityCustomCode).subscribe(
      (forecast: CityForecast) => {
        this.forecastInfo = forecast;
        console.log(this.forecastInfo);
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
      (res.main.temp_max - 273.15).toFixed(2)
    );
    const tempMin = this.forecastInfo.list.map(res =>
      (res.main.temp_min - 273.15).toFixed(2)
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
        weatherDates.push(this.formatDateToGraph(res));
      });
    return weatherDates;
  }

  formatDateToGraph(jsDateAsNumber: number): string {
    const jsDate = new Date(jsDateAsNumber * 1000);
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
