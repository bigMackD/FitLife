import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator } from '@angular/material';
import { Router } from '@angular/router';
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
  displayedColumns = ["name", "calories", "proteins", "carbs", "fats", "options"];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private productsService: ProductsService, public dialog: MatDialog,
    private router: Router) { }

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

  loadProductsPage(): void {
    this.dataSource.loadProducts(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  details(id: number): void {
    this.router.navigate(['/nutrition/product/' + id]);
  }
}
