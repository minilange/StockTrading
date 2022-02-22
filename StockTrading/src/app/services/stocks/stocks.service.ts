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
}
