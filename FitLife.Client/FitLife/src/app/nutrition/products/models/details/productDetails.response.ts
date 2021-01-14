import { IBaseResponse } from "src/app/shared/interfaces/base.response";
import { Product } from "../list/products.response";

export class ProductDetailsResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
    product: Product;
}