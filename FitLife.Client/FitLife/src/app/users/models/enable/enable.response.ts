import { IBaseResponse } from "src/app/shared/interfaces/base.response";

export class EnableUserResponse implements IBaseResponse{
    success: boolean;
    errors: string[];
}