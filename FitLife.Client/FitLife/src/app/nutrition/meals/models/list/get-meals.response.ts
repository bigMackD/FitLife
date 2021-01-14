import { IBaseResponse } from 'src/app/shared/interfaces/base.response';
import { IPagingBaseResponse } from 'src/app/shared/interfaces/pagingBase.response';

export class MealsResponse implements IBaseResponse, IPagingBaseResponse {
    count: number;
    success: boolean;
    errors: string[];
    meals: Meal[];
}

export interface Meal {
     id:number
     name:string
     calories:number
     proteinsGrams:number
     carbsGrams:number 
     fatsGrams:number 
}
