import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { config } from '../../../config';
import { Observable } from "rxjs";
import { MealCategoriesResponse } from "../models/list/getMealCategories.response";
import { ConfigurationService } from "src/app/shared/services/configuration.service";

@Injectable({
    providedIn: 'root'
  })
  export class MealCategoriesService {
  
    constructor(private httpClient: HttpClient,  private configurationService: ConfigurationService) { }

    public getAllMealCategories():Observable<MealCategoriesResponse>{
        return this.httpClient.get<MealCategoriesResponse>(this.configurationService.settings.apiUrl + '/MealCategories/');
      }

  }