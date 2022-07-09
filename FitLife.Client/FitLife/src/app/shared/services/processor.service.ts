import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { config } from "src/app/config";
import { CalculatePeriodicDietResponse } from "src/app/processor/models/calculatePeriodicDiet.response";

@Injectable({
    providedIn: 'root'
  })
  
export class ProcessorService {

    constructor(private httpClient: HttpClient) { }
  
    public calculatePeriodicDiet():Observable<CalculatePeriodicDietResponse>{
      return this.httpClient.post<CalculatePeriodicDietResponse>(config.baseUrl + '/Processor/periodicDiet', null);
    }
  }