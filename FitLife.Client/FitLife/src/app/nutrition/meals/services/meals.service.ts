import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ConfigurationService } from "src/app/shared/services/configuration.service";
import { config } from '../../../config';
import { AddMealRequest } from "../models/add/add-meal.request";
import { AddMealResponse } from "../models/add/add-meal.response";
import { DeleteMealRequest } from "../models/delete/delete-meal.request";
import { DeleteMealResponse } from "../models/delete/delete-meal.response";
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

  constructor(private httpClient: HttpClient,  private configurationService: ConfigurationService) { }

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
    return this.httpClient.post<AddMealResponse>(this.configurationService.settings.apiUrl + '/Meals/', request);
  }

  public getDetails(request: MealDetailsRequest): Observable<MealDetailsResponse> {
    return this.httpClient.get<MealDetailsResponse>(this.configurationService.settings.apiUrl + '/Meals/' + request.id);
  }

  public edit(request: EditMealRequest):Observable<EditMealResponse>{
    return this.httpClient.put<EditMealResponse>(this.configurationService.settings.apiUrl + '/Meals/'+request.id, request);
  }

  public delete(request: DeleteMealRequest):Observable<DeleteMealResponse>{
    return this.httpClient.delete<DeleteMealResponse>(this.configurationService.settings.apiUrl + '/Meals/'+request.id);
  }
}