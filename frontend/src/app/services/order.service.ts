import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OrderData } from '../interfaces/order-data';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(
    private http : HttpClient,

  ) { }

  placeOrder( orderData : OrderData ){
    console.log("Place order function mai aa rha! ", orderData);
    return this.http.post( 'https://localhost:7032/api/order', orderData ).pipe( 
      tap( (res) => {
        console.log("Order Service ke ander ka response hai ", res);
      } )
     );
  }
}
