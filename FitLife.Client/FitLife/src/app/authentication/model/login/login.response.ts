import { IBaseResponse } from 'src/app/shared/interfaces/base.response';

export class LoginResponse implements IBaseResponse{
    token: string;
    success: boolean;
    errors: string[];
}