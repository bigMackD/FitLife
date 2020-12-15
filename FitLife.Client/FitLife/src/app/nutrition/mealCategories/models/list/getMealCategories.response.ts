import { IBaseResponse } from "src/app/shared/interfaces/base.response";
import { IPagingBaseResponse } from "src/app/shared/interfaces/pagingBase.response";

export class MealCategoriesResponse implements IBaseResponse, IPagingBaseResponse {
    count: number;
    success: boolean;
    errors: string[];
    mealCategories: MealCategory[];
}

export interface MealCategory {
     id:number
     name:string
}