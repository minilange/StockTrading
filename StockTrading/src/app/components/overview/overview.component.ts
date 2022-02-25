import { DataSource } from '@angular/cdk/collections';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ChartComponent } from '../chart/chart.component';
import { IStock } from '../../interfaces/stock';
import { StocksService } from '../../services/stocks/stocks.service';

interface IEventEmit {
  val: string
}

@Component({
  selector: 'stock-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {

  @Output() selectedStockChanged: EventEmitter<string> = new EventEmitter<string>()


  displayedColumns: string[] = ["Name", "Ticker", "Price", "Issued", "Available"];
  dataSource: MatTableDataSource<IStock> = new MatTableDataSource();

  constructor(private stockService: StocksService) {
    stockService.getStock().subscribe(() => {
      setInterval(() => {
        stockService.getStock().subscribe(res => this.dataSource = new MatTableDataSource(res));
      }, 2500)
    })
  }

  ngOnInit(): void {
  }

  public stockSelectChange(variable: string): void {
    // this.chart.changeTicker(variable);
    this.selectedStockChanged.emit(variable)
  }
}
