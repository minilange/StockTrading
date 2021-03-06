import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { IStock } from '../../interfaces/stock';

@Injectable({
  providedIn: 'root'
})
export class StocksService {

  constructor(private http: HttpClient) { }

  getStock(): Observable<IStock[]> {
    return this.http.get<IStock[]>('/stock').pipe(
      map(res => {
        console.log(res)
        return res;
      })
    )
  }

  tradeStock(ticker: string, numTraded: number, operation: string) {
    let jsonObj: object = { Ticker: ticker, NumTraded: numTraded, Operation: operation };
    console.log(JSON.stringify(jsonObj), typeof(JSON.stringify(jsonObj)), "/api/Trade/" + ticker);
    this.http.put("/trade/" + ticker, JSON.stringify(jsonObj)).subscribe(resp => {
      console.log(resp);
    });
  }
}
