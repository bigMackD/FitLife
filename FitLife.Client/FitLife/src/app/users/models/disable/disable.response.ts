import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class DisableUserResponse implements IBaseResponse{
    success: boolean;
    errors: string[];
}