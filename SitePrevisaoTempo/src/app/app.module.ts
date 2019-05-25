import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AlertifyService } from './_services/alertify/alertify.service';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { CityService } from './_services/city/city.service';
import { CityListComponent } from './home/city-list/city-list.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      HeaderComponent,
      CityListComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule
   ],
   providers: [
      AlertifyService,
      CityService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
