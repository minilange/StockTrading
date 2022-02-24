import { PortalInjector } from '@angular/cdk/portal';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IPortfolio } from 'src/app/interfaces/portfolio';
import { StocksService } from 'src/app/services';
// import { PortfolioService } from 'src/app/services';
import { testPortfolio } from './testPortfolio';

interface IPortfolioData {
  ticker: string,
  price: number,
  amount: number,
  changePrice: number,
  changePercent: string
  // percentColor: string
}
@Component({
  selector: 'stock-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.css']
})
export class PortfolioComponent implements OnInit {

  displayedColumnsPort: string[] = ["Ticker", "Price", "Amount", "Change"];
  dataSourcePort: MatTableDataSource<IPortfolioData> = new MatTableDataSource()
  constructor(/*private portfolioService: PortfolioService*/) { 
    // transactionService.getTransactions().subscribe(res => {
      let res = testPortfolio;
      let data: IPortfolioData[] = []
      for (let point of res)
      {
        let currentPrice = 300;
        let changePriceTmp = Math.round(currentPrice - point.price);
        let changePercentTmp = `${Math.round((currentPrice / (point.price) - 1.0) * 100)}%`;
        let tmp: IPortfolioData = {
          ticker: point.ticker,
          price: point.price,
          amount: point.amount,
          changePrice: changePriceTmp,
          changePercent: changePercentTmp
          // percentColor: changePercentTmp == '-' ? "red" : "green"
        }
        data.push(tmp)
      }
      this.dataSourcePort = new MatTableDataSource(data);
    // })
    }
  ngOnInit(): void {
    
  }

}
