import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class AddUserMealResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}