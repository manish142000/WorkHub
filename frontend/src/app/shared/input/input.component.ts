import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms'

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent {
    @Input() controls : FormControl = new FormControl();
    @Input() placeholder : string = "";
    @Input() type : string = "";
    @Input() format = ''
}
