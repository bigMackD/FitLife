import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../../config';
import { Observable } from 'rxjs';
import { ProductsRequest } from '../models/list/products.request';
import { ProductsResponse } from '../models/list/products.response';
import { AddProductRequest } from '../models/add/addProduct.request';
import { AddProductResponse } from '../models/add/addProduct.response';
import { ProductDetailsRequest } from '../models/details/productDetails.request';
import { ProductDetailsResponse } from '../models/details/productDetails.response';
import { EditProductRequest } from '../models/edit/edit-product.request';
import { EditProductResponse } from '../models/edit/edit-product.response';
import { ConfigurationService } from 'src/app/shared/services/configuration.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private httpClient: HttpClient, private configurationService: ConfigurationService) { }

  public getProducts(request: ProductsRequest): Observable<ProductsResponse> {
    return this.httpClient.get<ProductsResponse>(config.baseUrl + '/Products/', {
      params: new HttpParams()
        // .set('filter', filter)
        .set('sortDirection', request.sortDirection)
        .set('pageIndex', request.pageIndex.toString())
        .set('pageSize', request.pageSize.toString())
    });
  }

  public getAllProducts(): Observable<ProductsResponse> {
    return this.httpClient.get<ProductsResponse>(this.configurationService.settings.apiUrl + '/Products/');
  }

  public add(request: AddProductRequest): Observable<AddProductResponse> {
    return this.httpClient.post<AddProductResponse>(this.configurationService.settings.apiUrl + '/Products/', request);
  }

  public getProductDetails(request: ProductDetailsRequest):Observable<ProductDetailsResponse>{
    return this.httpClient.get<ProductDetailsResponse>(this.configurationService.settings.apiUrl + '/Products/'+request.id);
  }

  public edit(request: EditProductRequest):Observable<EditProductResponse>{
    return this.httpClient.put<EditProductResponse>(this.configurationService.settings.apiUrl + '/Products/'+request.id, request);
  }
}