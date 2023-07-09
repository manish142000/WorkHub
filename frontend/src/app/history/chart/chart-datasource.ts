import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { PagingData } from "src/app/interfaces/paging-data";
import { BehaviorSubject, catchError, finalize } from "rxjs";
import { PageDataService } from "src/app/services/page-data.service";
import { Observable } from "rxjs";
import { PagingParams } from "src/app/interfaces/paging-params";
import {of} from 'rxjs'
import { PagingResponse } from "src/app/Responses/paging-response";

export class ChartDataSource implements DataSource<PagingData> {

    private OrdersSubject = new BehaviorSubject<PagingData[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public paging_Response = new BehaviorSubject<PagingResponse|null>(null);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private pagingService: PageDataService) {}

    connect(collectionViewer: CollectionViewer): Observable<PagingData[]> {
      return this.OrdersSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
      this.OrdersSubject.complete();
      this.loadingSubject.complete();
    }
  
    loadOrders( params : PagingParams )  {

      this.loadingSubject.next(true);

        this.pagingService.getData(params)?.pipe(
          catchError( () => of([]) ),
          finalize(()=>this.loadingSubject.next(false))).subscribe(
            (res) => {
              //console.log("subscriber ke ander bhi aa rha!", res);
              if( res ){
                this.paging_Response.next(res as PagingResponse);
                console.log("Paging data from backend ", (res as PagingResponse).pagingData);
                this.OrdersSubject.next((res as PagingResponse).pagingData);
              }
            }
          )
    }  

}