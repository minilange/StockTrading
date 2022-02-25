import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ITransaction } from 'src/app/interfaces/transaction';
import { TransactionsService } from 'src/app/services';
import { testData } from './testdata';

interface ITransactionData {
  timeStamp: string
  name: string
  transactor: string
  price: string
  percent: string
  percentColor: string
}

@Component({
  selector: 'stock-transaction-log',
  templateUrl: './transaction-log.component.html',
  styleUrls: ['./transaction-log.component.css']
})
export class TransactionLogComponent implements OnInit {

  displayedColumns: string[] = ["TimeStamp", "Name", "Transactor", "Price"];
  dataSource: MatTableDataSource<ITransactionData> = new MatTableDataSource()
  constructor(private transactionService: TransactionsService) {
    setInterval(() => {
      transactionService.getTransactions().subscribe(res => {
        let data: ITransactionData[] = []
        for (let point of res) {
          let tmpTimeStamp = point.timeStamp.split("T")
          let tmp: ITransactionData = {
            name: point.stock,
            price: `${point.oldPrice} -> ${point.newPrice}`,
            transactor: point.transactor,
            timeStamp: `${tmpTimeStamp[0]} - ${tmpTimeStamp[1]}`,
            percentColor: point.operation == "buy" ? "green" : "red",
            percent: `(${(((point.newPrice - point.oldPrice) / point.oldPrice) * 100).toFixed(2)}%)`
          }
          data.push(tmp)
        }
        this.dataSource = new MatTableDataSource(data);
      })
    },1000)
  }

  ngOnInit(): void {
  }

}
