import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'StockTrading';

  selectedStock: string = "";

  constructor() {
    this.changeStock("WEED");
   }

  changeStock(newStock: string) {
    this.selectedStock = (' ' + newStock).slice(1);
    console.log("Emitted " + this.selectedStock);
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}