import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IPortfolio } from '../../interfaces/portfolio';

@Injectable({
  providedIn: 'root'
})
export class PortfolioService {

  constructor(private http: HttpClient) { }

  getPortfolio(): Observable<IPortfolio[]> {
    return this.http.get<IPortfolio[]>('/portfolio').pipe(
      map(res => {
        return res;
      })
    )
  }
}
