import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class DeleteMealResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}