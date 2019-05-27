import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }

  formatDateFromNumber(jsDateAsNumber: number): string {
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

  tempFromKelvinToCelsiusStr(tempAsKelvin: number): string {
    return (tempAsKelvin - 273.15).toFixed(2);
  }
}
