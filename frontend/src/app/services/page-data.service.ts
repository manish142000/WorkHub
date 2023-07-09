import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PagingResponse } from '../Responses/paging-response';
import { PagingParams } from '../interfaces/paging-params';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';
import { PagingData } from '../interfaces/paging-data';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class PageDataService {

  constructor(
    private http : HttpClient
  ) { }

  getData(params : PagingParams){
    
    if( !params.startDate || !params.endDate ) return;

    return this.http.get<PagingResponse>('https://localhost:7032/api/order/paging', {
      params : new HttpParams() 
      .set('pageSize', params.pageSize!)
      .set('pageNo', params.pageNo)
      .set('startDate', params.startDate)
      .set('endDate', params.endDate)
    })

  }
}
