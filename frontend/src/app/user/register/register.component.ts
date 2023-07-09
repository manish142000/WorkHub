import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { ConfirmPassword } from 'src/app/shared/validators';
import { HttpClient } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { getHashedPassword } from 'src/app/shared/hashing';
import { SignUpData } from 'src/app/interfaces/sign-up-data';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

    /**
     *
     */
    constructor( 
      private http : HttpClient,
      private auth : AuthService,
      private route : Router 
    ) {
    }



    registerForm = new FormGroup({
      name : new FormControl("", [
        Validators.required,
        Validators.minLength(3)
      ]),
      email : new FormControl("", [
        Validators.email,
        Validators.required
      ]),
      age : new FormControl<number | null>(null, [
        Validators.required,
        Validators.min(18),
        Validators.max(120)
      ]),
      password : new FormControl("", [
        Validators.required,
        Validators.pattern(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/gm)
      ]),
      confirm_password : new FormControl("", [
        Validators.required
      ]),
      phoneno : new FormControl("", [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(10)
      ])
    })

    alert_msg = "";
    showAlert = false; 
    alert_color = 'blue';
    in_submission = false;

    setAlert(
      alert_msg : string, 
      alert_color : string, 
      showAlert : boolean, 
      in_submission : boolean 
    ){
      this.alert_msg = alert_msg; this.alert_color = alert_color; this.showAlert  = showAlert; this.in_submission = in_submission;
    }

    register(){

      this.setAlert("Please wait... Your account is being created", "blue", true, true);

      const form_data : SignUpData = {
        Name : this.registerForm.value.name,
        Email : this.registerForm.value.email,
        Age : this.registerForm.value.age,
        Password : this.registerForm.value.password,
        ConfirmPassword : this.registerForm.value.confirm_password,
        Phone : this.registerForm.value.phoneno
      };


       if( form_data.Password != form_data.ConfirmPassword ){
        this.setAlert("Passwords DO NOT Match", "red", true, false);
        return;
       }

      if( !form_data.Password || !form_data.ConfirmPassword ){
        return;
      }
       //encrypting password
       //form_data.Password = getHashedPassword(form_data.Password); 
       //form_data.ConfirmPassword = getHashedPassword(form_data.ConfirmPassword);

       console.log(form_data);
      
        this.auth.signUp(form_data).subscribe(
        (response) => { 
          console.log("Ye subscriber ke ander ka response hai! ", response);
          if( response.isSuccess == false ){
            this.setAlert("User Already Exists!", "red", true, false);
            this.registerForm.reset();
          }
          else {
            this.setAlert("Your account has been created!", "green", true, false);

            this.registerForm.reset();

            setTimeout( () => {
              this.setAlert("", "blue", false, false);          
            }, 2000 );

            setTimeout( () => {
              this.route.navigate(["history"]);
            }, 1500);
          }
        },
        (error) => { 
          console.log(error);
          this.setAlert("An error Occured! Please try Again!", "red", true, false);  
          this.registerForm.reset();          
        }
      )


    }
}
