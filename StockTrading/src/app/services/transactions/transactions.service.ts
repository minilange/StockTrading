import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ITransaction } from '../../interfaces/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private http: HttpClient) { }

  getTransactions(): Observable<ITransaction[]> {
    return this.http.get<ITransaction[]>('/transaction').pipe(
      map(res => {
        return res;
      })
    )
  }
}
