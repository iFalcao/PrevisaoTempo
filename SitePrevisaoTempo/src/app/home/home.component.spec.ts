/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HomeComponent } from './home.component';
import { CityListComponent } from './city-list/city-list.component';
import { CitySearchComponent } from './city-search/city-search.component';
import { Router } from '@angular/router';
import { AppRoutingModule } from '../app-routing.module';
import { ForecastComponent } from '../forecast/forecast.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ForecastCarouselComponent } from '../forecast/forecast-carousel/forecast-carousel.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  // const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AppRoutingModule,
        FormsModule,
        HttpClientTestingModule
      ],
      declarations: [
        HomeComponent,
        CityListComponent,
        CitySearchComponent,
        ForecastComponent,
        ForecastCarouselComponent
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
