import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { NotificationService } from '../shared/services/notification.service';
import { CategoryEnum } from './enums/category.enum';
import { GetUserMealsRequest } from './models/get-user-meals/get-user-meals.request';
import { UserMeal } from './models/get-user-meals/get-user-meals.response';
import { RegisterMealDialogComponent } from './register-meal-dialog/register-meal-dialog.component';
import { UserMealsService } from './services/user-meals.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit {
  viewDate = new Date();
  userMeals: UserMeal[];
  categoryEnum = CategoryEnum;

  breakfastMeals: UserMeal[] = [];
  lunchMeals: UserMeal[] = [];
  dinnerMeals: UserMeal[] = [];
  snackMeals: UserMeal[] = [];


  constructor(public dialog: MatDialog,
    private notificationService: NotificationService,
    private userMealsService: UserMealsService) { }

  ngOnInit() {
    this.getUserMealsByDate();
  }

  nextDay(){
    this.incrementDay(1)
    this.getUserMealsByDate();
  }

  previousDay(){
   this.incrementDay(-1)
   this.getUserMealsByDate();
  }

  private incrementDay(delta: number): void {
    this.viewDate = new Date(
      this.viewDate.getFullYear(),
      this.viewDate.getMonth(),
      this.viewDate.getDate() + delta);
  }

  registerMeal():void{
    let dialogRef = this.dialog.open(RegisterMealDialogComponent, {
      height: '150px',
      width: '600px',
      data: {
        date: this.viewDate
      }
    });

    dialogRef.afterClosed().subscribe(_ =>
     this.getUserMealsByDate() )
  }

  getUserMealsByDate():void{
    let request: GetUserMealsRequest = {
      consumedDate: this.viewDate
    }
    this.userMealsService.getByDate(request).subscribe(response =>
      {
        if (!response.success) {
          this.notificationService.error(response.errors);
        }
        else{
        this.userMeals = response.userMeals;
        this.sortMealsByCategory();
        }
      })
  }

  sortMealsByCategory():void {
  this.breakfastMeals = [];
  this.lunchMeals = [];
  this.dinnerMeals = [];
  this.snackMeals = [];
    this.userMeals.map(meal => {
      switch (meal.categoryId) {
        case CategoryEnum.Breakfast:
          this.breakfastMeals.push(meal);
          break;
        case CategoryEnum.Lunch:
          this.lunchMeals.push(meal);
          break;
        case CategoryEnum.Dinner:
          this.dinnerMeals.push(meal);
          break;
        case CategoryEnum.Snacks:
          this.snackMeals.push(meal);
          break;
        default:
          break;
      }
    })
  }

}
