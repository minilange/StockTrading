import { DataSource } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IStock } from '../../interfaces/stock';
import { StocksService } from '../../services/stocks/stocks.service';

@Component({
  selector: 'stock-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {

  displayedColumns: string[] = ["Name", "Ticker", "Price", "Issued", "Available"]
  dataSource: MatTableDataSource<IStock> = new MatTableDataSource();

  constructor(private stockService: StocksService) {
    stockService.getStock().subscribe(res => this.dataSource = new MatTableDataSource(res));
  }

  ngOnInit(): void {
  }

}
