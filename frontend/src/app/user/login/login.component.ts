import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { LoginData } from 'src/app/interfaces/login-data';
import { AuthService } from 'src/app/services/auth.service';
import { getHashedPassword } from 'src/app/shared/hashing';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

    constructor(
      private auth : AuthService
    ) {
            
    }

    LoginForm = new FormGroup({
      email : new FormControl<string | null>("", [
        Validators.required,
        Validators.email
      ]),
      password : new FormControl<string | null>("", [
        Validators.required
      ])
    })

    alert_msg = ""
    alert_color = "blue"
    showAlert = false;
    in_submission = false;

    setAlert(
      alert_msg : string | null | undefined, 
      alert_color : string, 
      showAlert : boolean, 
      in_submission : boolean 
    ){
      if( alert_msg != null && alert_msg != undefined ) this.alert_msg = alert_msg;
       
      this.alert_color = alert_color; this.showAlert  = showAlert; this.in_submission = in_submission;
    }

    login(){

      const data : LoginData = {
        Email : this.LoginForm.value.email,
        Password : this.LoginForm.value.password
      };

      if( !data.Password ){
        return;
      }

      //data.Password = getHashedPassword(data.Password); 

      //console.log("Yeh hai password ", data.Password);

      this.setAlert("Please wait! You are being logged in!", "black", true, true);

      this.auth.logIn( data ).subscribe( 
        ( response ) => {
          console.log("Response object hai ye  " + response);
          console.log("Object ka property hai ye " + response.jwtToken);
          if( response.isSuccess ){
            this.setAlert("You are now Logged In", "green", true, false);
            this.LoginForm.reset();
          }
          else{
            this.setAlert(response.errorMessages?.at(0), "red", true, false);
          }
        }
       )
    } 
}
