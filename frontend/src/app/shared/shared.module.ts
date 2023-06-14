import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { ModalComponent } from './modal/modal.component';
import { TabsComponent } from './tabs/tabs.component';
import { TabscontainerComponent } from './tabscontainer/tabscontainer.component';



@NgModule({
  declarations: [
    InputComponent,
    ModalComponent,
    TabsComponent,
    TabscontainerComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    InputComponent,
    ModalComponent,
    TabsComponent,
    TabscontainerComponent
  ]
})
export class SharedModule { }
