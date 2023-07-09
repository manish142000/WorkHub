import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ModalService } from 'src/app/services/modal.service';


@Component({
  selector: 'app-auth-modal',
  templateUrl: './auth-modal.component.html',
  styleUrls: ['./auth-modal.component.css']
})
export class AuthModalComponent implements OnInit {
  constructor( public auth : AuthService,
    public modal : ModalService ) {
    
  }

  ngOnInit(): void {
    this.auth.isAuthenticatedDelay$.subscribe(
      (val) => {
        console.log("authentication status ", val);
      }
    )
  }
}
