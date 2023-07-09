import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  visible : boolean = false;


  constructor( private route : Router ) { }

  ismodalOpen():boolean{
    return this.visible;
  }

  toggleModal(){
    this.visible = !this.visible;
  }

  closeModal(){
    this.visible = false;
    this.route.navigate([''])
  }
}
