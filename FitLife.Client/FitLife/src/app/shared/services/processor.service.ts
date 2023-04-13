import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CalculatePeriodicDietRequest } from "src/app/processor/models/calculatePeriodicDiet.request";
import { CalculatePeriodicDietResponse } from "src/app/processor/models/calculatePeriodicDiet.response";
import { ConfigurationService } from "./configuration.service";

@Injectable({
    providedIn: 'root'
  })
  
export class ProcessorService {

    constructor(private httpClient: HttpClient, private configurationService: ConfigurationService) { }
  
    public calculatePeriodicDiet(request: CalculatePeriodicDietRequest):Observable<CalculatePeriodicDietResponse>{
      return this.httpClient.post<CalculatePeriodicDietResponse>(this.configurationService.settings.apiUrl + '/Processor/periodicDiet/' + request.eventId, null);
    }
  }