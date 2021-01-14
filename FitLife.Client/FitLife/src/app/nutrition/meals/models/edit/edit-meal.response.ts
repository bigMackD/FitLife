import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class EditMealResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}