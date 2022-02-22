import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { NgApexchartsModule } from 'ng-apexcharts';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PortfolioComponent } from './components/portfolio/portfolio.component';
import { OverviewComponent } from './components/overview/overview.component';
import { ChartComponent } from './components/chart/chart.component';
import { TransactionLogComponent } from './components/transaction-log/transaction-log.component';
import { MatTableModule } from '@angular/material/table';


@NgModule({
  declarations: [
    AppComponent,
    PortfolioComponent,
    OverviewComponent,
    ChartComponent,
    TransactionLogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgApexchartsModule,
    MatCardModule,
    MatButtonModule,
    FlexLayoutModule,
    MatTableModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
