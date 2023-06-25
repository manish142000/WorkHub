import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {


    LoginForm = new FormGroup({
      email : new FormControl<string | null>("", [
        Validators.required,
        Validators.email
      ]),
      password : new FormControl("", [
        Validators.required
      ])
    })
}
