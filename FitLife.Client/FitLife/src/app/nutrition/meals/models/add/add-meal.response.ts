import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class AddMealResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}