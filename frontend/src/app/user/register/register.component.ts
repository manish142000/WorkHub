import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { ConfirmPassword } from 'src/app/shared/validators';
import { HttpClient } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import * as bcrypt from 'bcryptjs';

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
      private http : HttpClient 
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
      age : new FormControl("", [
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

    register(){
      this.alert_msg = "Please wait... Your account is being created";
      this.alert_color = "blue";
      this.showAlert = true;
      this.in_submission = true;

      var  {
        name, 
        email, 
        age, 
        password,
        confirm_password,
        phoneno
       } = this.registerForm.value 

       if( password != confirm_password ){
        this.alert_msg = "Passwords DO NOT Match";
        this.alert_color = "Red";
        this.showAlert = true;
        this.in_submission = false;

        return;
       }

       //encrypting password
       password = bcrypt.hashSync(password as string, 14); 
      

       var formdata = {
        'Name' : name, 
        'Email' : email,
        'Age' : age,
        'Password' : password,
        'ConfirmPassword' : password,
        'Phone' : phoneno
       }

       this.http.post('https://localhost:7032/api/employee', formdata).subscribe(
        (response) => { console.log(response) 
          this.alert_msg = "Your account has been created!";
          this.alert_color = "green";
          this.showAlert = true;
          this.in_submission = false; 

          setTimeout( () => {
            this.alert_msg = "";
            this.showAlert = false; 
            this.alert_color = 'blue';
            this.in_submission = false;            
          }, 2000 );
          
        },
        (error) => { console.log(error) 
        }
      )


    }
}
