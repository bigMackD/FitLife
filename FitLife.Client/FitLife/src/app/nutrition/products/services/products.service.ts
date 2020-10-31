import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../../config';
import { Observable } from 'rxjs';
import { ProductsRequest } from '../models/list/products.request';
import { ProductsResponse } from '../models/list/products.response';

@Injectable({
    providedIn: 'root'
  })
  export class ProductsService {
  
    constructor(private httpClient: HttpClient) { }
    public getProducts(request:ProductsRequest):Observable<ProductsResponse>{
        return this.httpClient.get<ProductsResponse>(config.baseUrl + '/Products/', {
          params: new HttpParams()
          // .set('filter', filter)
          .set('sortDirection', request.sortDirection)
          .set('pageIndex', request.pageIndex.toString())
          .set('pageSize', request.pageSize.toString())
        });
      }

//       public getDetails(request:ProductDetailsRequest):Observable<ProductDetailsResponse>{
//         return this.httpClient.get<ProductDetailsResponse>(config.baseUrl + '/Products/' + request.id);
//       }
  }