import { Component, OnInit } from '@angular/core';
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
import { HistoryService } from '../../services';
import { dataSeries } from "./DataSeries";

@Component({
  selector: 'stock-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})

export class ChartComponent implements OnInit {

    public series: ApexAxisChartSeries;
    public chart: ApexChart;
    public dataLabels: ApexDataLabels;
    public markers: ApexMarkers;
    public title: ApexTitleSubtitle;
    public fill: ApexFill;
    public yaxis: ApexYAxis;
    public xaxis: ApexXAxis;
    public tooltip: ApexTooltip;

  constructor(private historyService: HistoryService) {
    //this.initChartData();
    historyService.getHistory("WEED").subscribe(data => {
      let ts2 = 1484418600000;
      let dates = [];
      for (let i = 0; i < 120; i++) {
        ts2 = ts2 + 86400000;
        dates.push([ts2, data[0][i].value]);
      }

      this.series = [
        {
          name: "WEED",
          data: dates as []
        }
      ];
    })

    this.series = [];

      //let ts2 = 1484418600000;
      //let dates = [];
      //for (let i = 0; i < 120; i++) {
      //  ts2 = ts2 + 86400000;
      //  dates.push([ts2, dataSeries[1][i].value]);
      //}

      //this.series = [
      //  {
      //    name: "WEED",
      //    data: dates as []
      //  }
      //];
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
            return (val / 1000000).toFixed(0);
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
            return (val / 1000000).toFixed(0);
          }
        }
      };
    }

    public initChartData(): void {
        
    }

    ngOnInit() {

    }
}
