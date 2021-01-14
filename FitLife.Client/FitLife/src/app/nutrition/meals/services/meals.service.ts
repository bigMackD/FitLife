import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { config } from '../../../config';
import { AddMealRequest } from "../models/add/add-meal.request";
import { AddMealResponse } from "../models/add/add-meal.response";
import { MealDetailsRequest } from "../models/details/meal-details-request";
import { MealDetailsResponse } from "../models/details/meal-details.response";
import { EditMealRequest } from "../models/edit/edit-meal.request";
import { EditMealResponse } from "../models/edit/edit-meal.response";
import { MealsRequest } from "../models/list/get-meals.request";
import { MealsResponse } from "../models/list/get-meals.response";

@Injectable({
  providedIn: 'root'
})
export class MealsService {

  constructor(private httpClient: HttpClient) { }

  public getMeals(request: MealsRequest): Observable<MealsResponse> {
    return this.httpClient.get<MealsResponse>(config.baseUrl + '/Meals/', {
      params: new HttpParams()
        // .set('filter', filter)
        .set('sortDirection', request.sortDirection)
        .set('pageIndex', request.pageIndex.toString())
        .set('pageSize', request.pageSize.toString())
    });
  }


  public addMeal(request: AddMealRequest): Observable<AddMealResponse> {
    return this.httpClient.post<AddMealResponse>(config.baseUrl + '/Meals/', request);
  }

  public getDetails(request: MealDetailsRequest): Observable<MealDetailsResponse> {
    return this.httpClient.get<MealDetailsResponse>(config.baseUrl + '/Meals/' + request.id);
  }

  public edit(request: EditMealRequest):Observable<EditMealResponse>{
    return this.httpClient.put<EditMealResponse>(config.baseUrl + '/Meals/'+request.id, request);
  }
}