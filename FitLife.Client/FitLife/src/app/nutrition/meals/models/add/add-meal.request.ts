import { MealProductModel } from "../shared/mealProduct.model";


export class AddMealRequest {
    constructor(public name: string,
        public categoryId: number,
        public mealProducts: AddMealProductRequestModel[]
    ) { }
}

export class AddMealProductRequestModel {
    constructor(
        public id: number,
        public grams: number = 0
    ) { }
}