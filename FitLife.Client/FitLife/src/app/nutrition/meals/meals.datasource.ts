import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { BehaviorSubject, Observable } from 'rxjs';
import {finalize } from 'rxjs/operators/'; 
import { MealsRequest } from "./models/list/get-meals.request";
import { Meal } from "./models/list/get-meals.response";
import { MealsService } from "./services/meals.service";


export class MealsDataSource implements DataSource<Meal> {

    private mealsSubject = new BehaviorSubject<Meal[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();
    public count: number;

    constructor(private mealsService: MealsService) {}

    connect(collectionViewer: CollectionViewer): Observable<Meal[]> {
     return this.mealsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
      this.mealsSubject.complete();
      this.loadingSubject.complete();
    }
  
    loadMeals(filter: string = '', sortDirection: string = 'asc', pageIndex: number = 0, pageSize: number = 10) {
                  const request: MealsRequest = {
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    sortDirection: sortDirection
                  }
     this.mealsService.getMeals(request)
     .pipe(
        finalize(() => this.loadingSubject.next(false)))
    .subscribe(response =>{
      this.count = response.count;
      this.mealsSubject.next(response.meals);
    }); 
    }  
}