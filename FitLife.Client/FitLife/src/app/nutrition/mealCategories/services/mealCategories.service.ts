import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { config } from '../../../config';
import { Observable } from "rxjs";
import { MealCategoriesResponse } from "../models/list/getMealCategories.response";

@Injectable({
    providedIn: 'root'
  })
  export class MealCategoriesService {
  
    constructor(private httpClient: HttpClient) { }

    public getAllMealCategories():Observable<MealCategoriesResponse>{
        return this.httpClient.get<MealCategoriesResponse>(config.baseUrl + '/MealCategories/');
      }

  }