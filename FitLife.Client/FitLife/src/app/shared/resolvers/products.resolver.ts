import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { ProductsResponse } from "src/app/nutrition/products/models/list/products.response";
import { ProductsService } from "src/app/nutrition/products/services/products.service";

@Injectable({
    providedIn: 'root'
  })
  
  export class ProductsResolver implements Resolve<Observable<ProductsResponse>> {
    constructor(private service:ProductsService) {
    }
    resolve() {
      return this.service.getAllProducts();
    }
  }