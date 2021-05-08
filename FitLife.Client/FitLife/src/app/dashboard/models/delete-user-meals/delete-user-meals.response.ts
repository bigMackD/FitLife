import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class DeleteUserMealResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}