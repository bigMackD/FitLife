import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator } from '@angular/material';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MealsDataSource } from './meals.datasource';
import { DeleteMealRequest } from './models/delete/delete-meal.request';
import { DeleteMealResponse } from './models/delete/delete-meal.response';
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
     private router: Router,  private notificationService: NotificationService) { }

  ngOnInit() {
    this.dataSource = new MealsDataSource(this.mealsService);
    this.dataSource.loadMeals();
  }

  ngAfterViewInit() {
    this.paginator.page
      .pipe(
        tap(() => this.loadMealsPage())
      )
      .subscribe();
  }

  loadMealsPage() {
    this.dataSource.loadMeals(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  details(id: number): void {
    this.router.navigate(['/nutrition/meal/' + id]);
  }

  delete(id: number): void {
  var request: DeleteMealRequest = {
    id: id
  };
  this.mealsService.delete(request).subscribe(response => 
    this.handleDeleteResponse(response));
  }

  handleDeleteResponse(response:DeleteMealResponse){
    if (!response.success) {
      this.notificationService.error(response.errors);
    }
    else {
      this.notificationService.success('Meal succesfully deleted!');
      this.loadMealsPage();
    }
  }
}
