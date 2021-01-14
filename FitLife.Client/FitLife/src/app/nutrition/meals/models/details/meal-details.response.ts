import { IBaseResponse } from "src/app/shared/interfaces/base.response";
import { MealCategoryModel } from "../shared/mealCategory.model";

export class MealDetailsResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
    meal: MealDetails;
}

export class MealDetails {
    id: number;
    name: string;
    proteinsGrams: number;
    carbsGrams: number;
    fatsGrams: number;
    mealProducts: MealProductDetailsResponse[];
    category: MealCategoryModel;
}

export class MealProductDetailsResponse {
    constructor(
        public productId: number,
        public name: string,
        public calories: number,
        public proteinsGrams: number,
        public carbsGrams: number,
        public fatsGrams: number,
        public grams: number = 0
    ) { }

}