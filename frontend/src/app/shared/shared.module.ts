import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { ModalComponent } from './modal/modal.component';
import { TabsComponent } from './tabs/tabs.component';
import { TabscontainerComponent } from './tabscontainer/tabscontainer.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { provideEnvironmentNgxMask, NgxMaskDirective } from 'ngx-mask';
import { AlertComponent } from './alert/alert.component';

@NgModule({
  declarations: [
    InputComponent,
    ModalComponent,
    TabsComponent,
    TabscontainerComponent,
    AlertComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgxMaskDirective
  ],
  exports: [
    InputComponent,
    ModalComponent,
    TabsComponent,
    TabscontainerComponent,
    AlertComponent
  ]
})
export class SharedModule { }
