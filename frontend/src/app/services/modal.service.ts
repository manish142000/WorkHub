import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  visible : boolean = false;


  constructor() { }

  ismodalOpen():boolean{
    return this.visible;
  }

  toggleModal(){
    this.visible = !this.visible;
  }

  closeModal(){
    this.visible = false;
  }
}
