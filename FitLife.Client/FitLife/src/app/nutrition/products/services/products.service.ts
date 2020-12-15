import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../../config';
import { Observable } from 'rxjs';
import { ProductsRequest } from '../models/list/products.request';
import { ProductsResponse } from '../models/list/products.response';
import { AddProductRequest } from '../models/add/addProduct.request';
import { AddProductResponse } from '../models/add/addProduct.response';

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

      public getAllProducts():Observable<ProductsResponse>{
        return this.httpClient.get<ProductsResponse>(config.baseUrl + '/Products/');
      }

public addProduct(request:AddProductRequest):Observable<AddProductResponse>{
  return this.httpClient.post<AddProductResponse>(config.baseUrl + '/Products/', request);
}
  }