import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator } from '@angular/material';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { MealsDataSource } from './meals.datasource';
import { MealsService } from './services/meals.service';

@Component({
  selector: 'app-meals',
  templateUrl: './meals.component.html',
  styleUrls: ['./meals.component.sass']
})
export class MealsComponent implements OnInit {
  dataSource: MealsDataSource;
  displayedColumns = ["name", "calories", "proteins", "carbs", "fats", "options"];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private mealsService: MealsService, public dialog: MatDialog,
     private router: Router) { }

  ngOnInit() {
    this.dataSource = new MealsDataSource(this.mealsService);
    this.dataSource.loadMeals();
  }

  ngAfterViewInit() {
    this.paginator.page
      .pipe(
        tap(() => this.loadProductsPage())
      )
      .subscribe();
  }

  loadProductsPage() {
    this.dataSource.loadMeals(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  details(id: number): void {
    this.router.navigate(['/nutrition/meal/' + id]);
  }
}
