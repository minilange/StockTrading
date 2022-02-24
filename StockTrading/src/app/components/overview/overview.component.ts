import { DataSource } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ChartComponent } from '../chart/chart.component';
import { IStock } from '../../interfaces/stock';
import { StocksService } from '../../services/stocks/stocks.service';

@Component({
  providers: [ChartComponent],
  selector: 'stock-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {

  displayedColumns: string[] = ["Name", "Ticker", "Price", "Issued", "Available"]
  dataSource: MatTableDataSource<IStock> = new MatTableDataSource();

  constructor(private stockService: StocksService, private chart: ChartComponent) {
    stockService.getStock().subscribe(res => this.dataSource = new MatTableDataSource(res));
  }

  ngOnInit(): void {
  }

  public stockSelectChange(variable: string): void{
    this.chart.changeTicker(variable);
  }
}
