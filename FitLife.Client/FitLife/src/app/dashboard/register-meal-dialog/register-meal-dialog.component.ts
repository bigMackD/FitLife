import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MealCategoriesService } from 'src/app/nutrition/mealCategories/services/mealCategories.service';
import { MealsRequest } from 'src/app/nutrition/meals/models/list/get-meals.request';
import { Meal } from 'src/app/nutrition/meals/models/list/get-meals.response';
import { MealCategoryModel } from 'src/app/nutrition/meals/models/shared/mealCategory.model';
import { MealsService } from 'src/app/nutrition/meals/services/meals.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { AddUserMealRequest } from '../models/add-user-meal/add-user-meal.resquest';
import { UserMealsService } from '../services/user-meals.service';

@Component({
  selector: 'app-register-meal-dialog',
  templateUrl: './register-meal-dialog.component.html',
  styleUrls: ['./register-meal-dialog.component.sass']
})
export class RegisterMealDialogComponent implements OnInit {
  
  mealCategories: MealCategoryModel[];
  meals: Meal[] = [{name: 'dsa', id: 1, calories: 100, proteinsGrams:1, carbsGrams:1, fatsGrams:2}]
  selectedCategory: number;
  selectedMeal:number;
  dateConsumed: Date;

  constructor( public dialogRef: MatDialogRef<RegisterMealDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
   private mealsService: MealsService,
  private categoriesService:MealCategoriesService,
  private userMealsService: UserMealsService,
  private notificationService: NotificationService) { }

  ngOnInit() {
    this.dateConsumed = this.data.date as Date;
    let mealsRequest:MealsRequest = {
      pageIndex: 0,
      //TODO
      pageSize: 1000,
      sortDirection: "desc"
    }
    this.mealsService.getMeals(mealsRequest).subscribe(response =>
     {
      if (!response.success) {
        this.notificationService.error(response.errors);
      }
      else{
         this.meals = response.meals;
       }
     })

     this.categoriesService.getAllMealCategories().subscribe(response => {
      if (!response.success) {
        this.notificationService.error(response.errors);
      } 
      else{
         this.mealCategories = response.mealCategories;
       }
     })
  }

  submit(){
 let request: AddUserMealRequest = {
   categoryId: this.selectedCategory,
   mealId: this.selectedMeal,
   consumedDate: this.dateConsumed
 }
 this.userMealsService.add(request).subscribe(response =>
   {
    if (!response.success) {
      this.notificationService.error(response.errors);
    }
    else{
      this.notificationService.success('Meal registered!');
      this.dialogRef.close();
    }
   }
 )
  }
  

}
