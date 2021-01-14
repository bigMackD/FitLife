import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class EditProductResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}