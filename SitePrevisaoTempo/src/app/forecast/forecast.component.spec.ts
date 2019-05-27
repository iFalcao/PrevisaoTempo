/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ForecastComponent } from './forecast.component';
import { HeaderComponent } from '../header/header.component';
import { AppRoutingModule } from '../app-routing.module';
import { HomeComponent } from '../home/home.component';
import { CityListComponent } from '../home/city-list/city-list.component';
import { CitySearchComponent } from '../home/city-search/city-search.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ForecastCarouselComponent } from './forecast-carousel/forecast-carousel.component';

describe('ForecastComponent', () => {
  let component: ForecastComponent;
  let fixture: ComponentFixture<ForecastComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AppRoutingModule,
        FormsModule,
        HttpClientTestingModule
      ],
      declarations: [
        ForecastComponent,
        HeaderComponent,
        HomeComponent,
        CityListComponent,
        CitySearchComponent,
        ForecastCarouselComponent
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ForecastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


});
