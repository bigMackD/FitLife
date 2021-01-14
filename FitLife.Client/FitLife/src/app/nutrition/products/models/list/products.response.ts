import { IBaseResponse } from 'src/app/shared/interfaces/base.response';
import { IPagingBaseResponse } from 'src/app/shared/interfaces/pagingBase.response';

export class ProductsResponse implements IBaseResponse, IPagingBaseResponse {
    count: number;
    success: boolean;
    errors: string[];
    products: Product[];
}

export interface Product {
     id:number
     name:string
     calories:number
     proteinsGrams:number
     carbsGrams:number 
     fatsGrams:number 
}

export class MealProduct {
    constructor(
        public id: number,
        public name: string,
        public calories: number,
        public proteinsGrams: number,
        public carbsGrams: number,
        public fatsGrams: number,
        public grams: number = 0
    ) { }
}