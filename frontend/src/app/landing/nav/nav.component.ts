import { Component } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';
import { AuthService } from 'src/app/services/auth.service';
import { OrderFormService } from 'src/app/services/order-form.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  constructor(
    public modal : ModalService,
    public auth : AuthService,
    public form : OrderFormService,
    private route : Router
  ){}

  openModal($event : Event){
    
    event?.preventDefault();

    this.modal.toggleModal();

    this.route.navigate(["authenticate"])
  }

  logout(){
    this.auth.onLogout();
    this.route.navigate(["authenticate"])
  }

  ngOnInit(): void {
    this.auth.isAuthenticatedDelay$.subscribe(
      (val) => {
        console.log("authentication status ", val);
      }
    )
  }
}
