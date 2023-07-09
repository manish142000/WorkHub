import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ChartDataSource } from './chart-datasource';
import { PageDataService } from 'src/app/services/page-data.service';
import { PagingParams } from 'src/app/interfaces/paging-params';
import { ActivatedRoute } from '@angular/router';
import { PagingResponse } from 'src/app/Responses/paging-response';
import { tap } from 'rxjs/operators';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit, AfterViewInit {
  orders: PagingResponse | undefined;
  dataSource: ChartDataSource;
  displayedColumns = ["Breakfast", "Lunch", "Day", "Date"];

  days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  constructor(private pagingService : PageDataService, private route : ActivatedRoute) {
    this.dataSource = new ChartDataSource(this.pagingService); 
  }

  ngOnInit() {
      const initial : PagingParams = {
        pageSize : 3,
        pageNo : 0,
        startDate : "2023-01-28",
        endDate : "2023-12-28"
      }
      //console.log(this.route.snapshot.data);
      this.dataSource = new ChartDataSource(this.pagingService);
      this.dataSource.loadOrders(initial);

      this.dataSource.paging_Response.subscribe( (val) => {
        if( val )
        this.orders = val;
      } )
  }


   dateForm = new FormGroup({
    fromDate : new FormControl<string | null>("", [
        Validators.required   
    ]),
    toDate : new FormControl<string | null>("", [
      Validators.required
    ])
  });

  ngAfterViewInit(): void {
    this.paginator?.page
    .pipe(
        tap(() => this.loadDatapage())
    )
    .subscribe();
  }

  loadDatapage(){

    const page_parameters : PagingParams = {
      pageSize : this.paginator?.pageSize,
      pageNo : this.paginator?.pageIndex!,
      startDate : "2023-01-28",
      endDate : "2023-12-28"     
    }

    this.dataSource.loadOrders( page_parameters );
  }
}
