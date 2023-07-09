import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { OrderData } from 'src/app/interfaces/order-data';
import { OrderService } from 'src/app/services/order.service';
import { AuthService } from 'src/app/services/auth.service';
import { OrderFormService } from 'src/app/services/order-form.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  date : Date;
  date1 : Date;
  date2 : Date;
  date3 : Date;
  date4 : Date;
  date5: Date;
  date6 : Date;
  date7: Date;



  constructor(
    public auth : AuthService,
    private Order : OrderService,
    public form : OrderFormService
  ) {
    this.date = new Date();
    this.date1 = new Date();
    this.date2 = new Date(this.date.setDate(this.date.getDate() + 1));
    this.date3 = new Date(this.date.setDate(this.date.getDate() + 1));
    this.date4 = new Date(this.date.setDate(this.date.getDate() + 1));
    this.date5 = new Date(this.date.setDate(this.date.getDate() + 1));
    this.date6 = new Date(this.date.setDate(this.date.getDate() + 1));
    this.date7 = new Date(this.date.setDate(this.date.getDate() + 1));
  }

  orderForm = new FormGroup({
    createdDate : new FormControl("", [
      Validators.required
    ]),
    breakFast : new FormControl("",[
      Validators.required
    ]),
    lunch : new FormControl("", [
      Validators.required
    ])
  });

  order(){
    //console.log(this.orderForm.value);

    this.auth.user.subscribe(
      (user) => {
        const formData : OrderData = {
          userEmail : user?.email,
          dateCreated : new Date(this.orderForm.value.createdDate as string), 
          dayCreated : new Date(this.orderForm.value.createdDate as string).getDay(),
          lunch : this.orderForm.value.lunch,
          breakfast : this.orderForm.value.breakFast     
        }

        this.Order.placeOrder(formData).subscribe(
          (res) => {
            this.orderForm.reset();
          }
          ,
          (err) => {
            alert(err);
          }
        )

      }
    )


  }
}
