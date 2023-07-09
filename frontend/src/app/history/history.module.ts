import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartComponent } from './chart/chart.component';
import { MatTableModule  } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';  

@NgModule({
  declarations: [
    ChartComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    FormsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    ReactiveFormsModule
  ]
  ,
  exports : [
    ChartComponent
  ]
})
export class HistoryModule { }
