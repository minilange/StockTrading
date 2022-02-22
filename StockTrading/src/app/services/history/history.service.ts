import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IHistory } from '../../interfaces/history';

interface IChartData {
  date: string
  value: number
}

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor(private http: HttpClient) { } 

  getHistory(ticker: string): Observable<IChartData[][]> {
    return this.http.get<IHistory[]>('/history?ticker=' + ticker).pipe(
      map(res => {
        console.log(res);
        let charData: IChartData[][] = [];
        for (let record of res) {
          let tmp: IChartData = {
            date: record.TimeStamp,
            value: record.Price
          };
          charData[0].push(tmp);
        }
        return charData;
      })
    )
  }
}
