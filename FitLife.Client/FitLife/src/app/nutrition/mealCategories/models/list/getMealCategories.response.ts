import { IBaseResponse } from "src/app/shared/interfaces/base.response";
import { IPagingBaseResponse } from "src/app/shared/interfaces/pagingBase.response";
import { MealCategoryModel } from "../../../meals/models/shared/mealCategory.model";

export class MealCategoriesResponse implements IBaseResponse, IPagingBaseResponse {
    count: number;
    success: boolean;
    errors: string[];
    mealCategories: MealCategoryModel[];
}

