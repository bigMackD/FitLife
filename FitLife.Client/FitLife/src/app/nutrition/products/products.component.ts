import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator } from '@angular/material';
import { tap } from 'rxjs/operators';
import { ProductsDataSource } from './products.datasource';
import { ProductsService } from './services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.sass']
})
export class ProductsComponent implements OnInit {

  dataSource: ProductsDataSource;
  displayedColumns= [ "name", "calories", "proteins", "carbs", "fats", "options"];

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  constructor(private productsService:ProductsService, public dialog: MatDialog) { }

  ngOnInit() {
    this.dataSource = new ProductsDataSource(this.productsService);
    this.dataSource.loadProducts();
  }

  ngAfterViewInit() {
    this.paginator.page
        .pipe(
            tap(() => this.loadProductsPage())
        )
        .subscribe();
}
  
loadProductsPage() {
  this.dataSource.loadProducts(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
}

}
