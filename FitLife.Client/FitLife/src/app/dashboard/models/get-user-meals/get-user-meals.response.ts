import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class GetUserMealsResponse implements IBaseResponse {
    count: number;
    success: boolean;
    errors: string[];
    userMeals: UserMeal[]
}

export interface UserMeal {
    id:number
    name:string
    calories:number
    proteinsGrams:number
    carbsGrams:number 
    fatsGrams:number 
    categoryId:number 
}