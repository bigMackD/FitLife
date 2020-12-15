import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { MealCategoriesResponse } from "src/app/nutrition/mealCategories/models/list/getMealCategories.response";
import { MealCategoriesService } from "src/app/nutrition/mealCategories/services/mealCategories.service";

@Injectable({
    providedIn: 'root'
  })
  
  export class MealCategoriesResolver implements Resolve<Observable<MealCategoriesResponse>> {
    constructor(private service:MealCategoriesService) {
    }
    resolve() {
      return this.service.getAllMealCategories();
    }
  }