import { AddMealProductRequestModel } from "../add/add-meal.request";

export class EditMealRequest{
    constructor(public id:number,
                public name: string,
                public categoryId: number,
                public mealProducts: AddMealProductRequestModel[]
                ) { }
}
