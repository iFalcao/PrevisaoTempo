/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ForecastCarouselComponent } from './forecast-carousel.component';
import { ForecastComponent } from '../forecast.component';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { HomeComponent } from 'src/app/home/home.component';
import { CitySearchComponent } from 'src/app/home/city-search/city-search.component';
import { CityListComponent } from 'src/app/home/city-list/city-list.component';
import { FormsModule } from '@angular/forms';

describe('ForecastCarouselComponent', () => {
  let component: ForecastCarouselComponent;
  let fixture: ComponentFixture<ForecastCarouselComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AppRoutingModule,
        FormsModule
      ],
      declarations: [
        ForecastCarouselComponent,
        ForecastComponent,
        HomeComponent,
        CitySearchComponent,
        CityListComponent
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ForecastCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


});
