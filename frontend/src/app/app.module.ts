import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingModule } from './landing/landing.module';
import { UserModule } from './user/user.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule, HttpInterceptor } from '@angular/common/http';
import { OrderModule } from './order/order.module';
import { AuthInterceptorInterceptor } from './interceptors/auth-interceptor.interceptor';
import { CookieService } from 'ngx-cookie-service';
import { HistoryModule } from './history/history.module';
import { MatTableModule  } from '@angular/material/table';
import {MatInputModule} from '@angular/material/input';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from './landing/nav/nav.component';
import { OrderComponent } from './order/order/order.component';
import { ChartComponent } from './history/chart/chart.component';
import { AuthModalComponent } from './user/auth-modal/auth-modal.component';
import { ProfileComponent } from './shared/profile/profile.component';
import { SharedModule } from './shared/shared.module';

const appRoute : Routes = [ 
  { path : "authenticate", component : AuthModalComponent },
  { path : "order", component : OrderComponent },
  { path : "history", component : ChartComponent },
]

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LandingModule,
    UserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    OrderModule,
    HistoryModule,
    MatTableModule,
    MatInputModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSortModule,
    SharedModule,
    RouterModule.forRoot(appRoute)
  ],
  providers: [
    { 
      provide : HTTP_INTERCEPTORS,  
      useClass : AuthInterceptorInterceptor,
      multi: true
    },
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
