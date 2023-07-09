import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderFormService {

  visible : Boolean | undefined

  constructor() { }

  get isOrderOpen(){
    return this.visible;
  }

  toggleOrder(){
    this.visible = !this.visible;
  }

}
