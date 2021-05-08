import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { config } from '../../config';
import { Observable } from "rxjs";
import { AddUserMealResponse } from "../models/add-user-meal/add-user-meal.response";
import { AddUserMealRequest } from "../models/add-user-meal/add-user-meal.resquest";
import { GetUserMealsResponse } from "../models/get-user-meals/get-user-meals.response";
import { GetUserMealsRequest } from "../models/get-user-meals/get-user-meals.request";
import { DeleteUserMealResponse } from "../models/delete-user-meals/delete-user-meals.response";
import { DeleteUserMealsRequest } from "../models/delete-user-meals/delete-user-meals.request";

@Injectable({
  providedIn: 'root'
})
export class UserMealsService {

  constructor(private httpClient: HttpClient) { }

  public add(request: AddUserMealRequest): Observable<AddUserMealResponse> {
    return this.httpClient.post<AddUserMealResponse>(config.baseUrl + '/UserMeals/', request);
  }

  public getByDate(request: GetUserMealsRequest): Observable<GetUserMealsResponse> {
    return this.httpClient.get<GetUserMealsResponse>(config.baseUrl + '/UserMeals/'+ request.consumedDate.toUTCString());
  }

  public delete(request: DeleteUserMealsRequest): Observable<DeleteUserMealResponse> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: request
    };
    return this.httpClient.delete<DeleteUserMealResponse>(config.baseUrl + '/UserMeals/delete', options)
  }
}