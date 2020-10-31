import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from 'rxjs';
import {catchError, finalize } from 'rxjs/operators/'; 
import { ProductsRequest } from './models/list/products.request';
import { Product } from './models/list/products.response';
import { ProductsService } from './services/products.service';

export class ProductsDataSource implements DataSource<Product> {

    private productsSubject = new BehaviorSubject<Product[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();
    public count: number;

    constructor(private productsService: ProductsService) {}

    connect(collectionViewer: CollectionViewer): Observable<Product[]> {
     return this.productsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
      this.productsSubject.complete();
      this.loadingSubject.complete();
    }
  
    loadProducts(filter: string = '',
                sortDirection: string = 'asc', pageIndex: number = 0, pageSize: number = 10) {
                  const request: ProductsRequest = {
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    sortDirection: sortDirection
                  }
     this.productsService.getProducts(request)
     .pipe(
        finalize(() => this.loadingSubject.next(false)))
    .subscribe(response =>{
      this.count = response.count;
      this.productsSubject.next(response.products);
    }); 
    }  
}