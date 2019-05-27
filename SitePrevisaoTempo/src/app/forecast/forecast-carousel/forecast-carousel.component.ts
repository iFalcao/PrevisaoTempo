import { Component, OnInit, Input } from '@angular/core';
import { CityForecast } from 'src/app/_models/city-forecast';
import { CommonService } from 'src/app/_services/common.service';
declare let $: any;

@Component({
  selector: 'app-forecast-carousel',
  templateUrl: './forecast-carousel.component.html',
  styleUrls: ['./forecast-carousel.component.css']
})
export class ForecastCarouselComponent implements OnInit {
  @Input() cityForecast: CityForecast;

  constructor(private commonService: CommonService) { }

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
  }

  formatDateToGraph(jsDateAsNumber: number): string {
    return this.commonService
      .formatDateFromNumber(jsDateAsNumber);
  }

  fixTemperature(tempAsKelvin: number): string {
    return this.commonService
      .tempFromKelvinToCelsiusStr(tempAsKelvin);
  }
}
