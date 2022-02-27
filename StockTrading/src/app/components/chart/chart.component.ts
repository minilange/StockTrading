import { Component, Input, OnInit } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexFill,
  ApexMarkers,
  ApexYAxis,
  ApexXAxis,
  ApexTooltip
} from "ng-apexcharts";
import { HistoryService, StocksService } from '../../services';

@Component({
  selector: 'stock-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})

export class ChartComponent implements OnInit {

  @Input() set selectedStock(stock: string) {
    console.log("Selected: " + stock)
    this._selected = stock;
    this.changeSelectedStock(this._selected);
  };

  public series: ApexAxisChartSeries;
  public chart: ApexChart;
  public dataLabels: ApexDataLabels;
  public markers: ApexMarkers;
  public title: ApexTitleSubtitle;
  public fill: ApexFill;
  public yaxis: ApexYAxis;
  public xaxis: ApexXAxis;
  public tooltip: ApexTooltip;

  _selected: string = "WEED"

  constructor(private historyService: HistoryService, private stockService: StocksService) {
    historyService.getHistory(this._selected).subscribe(() => {
      setInterval(() => {
        this.changeSelectedStock(this._selected);
      }, 5000)
    })

    this.series = [];

    this.chart = {
      type: "area",
      stacked: false,
      height: 350,
      zoom: {
        type: "x",
        enabled: true,
        autoScaleYaxis: true
      },
      toolbar: {
        autoSelected: "zoom"
      }
    };
    this.dataLabels = {
      enabled: false
    };
    this.markers = {
      size: 0
    };
    this.title = {
      text: "Stock Price Movement",
      align: "left"
    };
    this.fill = {
      type: "gradient",
      gradient: {
        shadeIntensity: 1,
        inverseColors: false,
        opacityFrom: 0.5,
        opacityTo: 0,
        stops: [0, 90, 100]
      }
    };
    this.yaxis = {
      labels: {
        formatter: function (val) {
          return val.toFixed(2);
        }
      },
      title: {
        text: "Price"
      }
    };
    this.xaxis = {
      type: "datetime"
    };
    this.tooltip = {
      shared: false,
      y: {
        formatter: function (val) {
          return val.toFixed(2);
        }
      }
    };
  }

  ngOnInit() {

  }

  buyStock(amount: number) {
    console.log(`Buying ${amount} shares of ${this._selected}`);
    this.stockService.tradeStock(this._selected, 100, "buy");
  }

  sellStock(amount: number) {
    console.log(`Selling ${amount} shares of ${this._selected}`);
    this.stockService.tradeStock(this._selected, 100, "sell");
  }

  changeSelectedStock(stock: string) {
    this.historyService.getHistory(stock).subscribe(data => {
      let ts2 = 0;
      let dates = [];
      for (let point of data) {
        let tmp = new Date(point.date);
        ts2 = tmp.getTime();
        dates.push([ts2, point.value]);
      }
      this.series = [
        {
          name: stock,
          data: dates as []
        }
      ];
      console.log(stock)
    })
  }
}
