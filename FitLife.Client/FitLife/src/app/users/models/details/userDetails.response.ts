import { IBaseResponse } from 'src/app/shared/interfaces/base.response';

export class UserDetailsResponse implements IBaseResponse{
    success: boolean;
    errors: string[];
    email: string;
    fullName: string;
    phoneNumber: string;
    userName: string;
    twoFactorEnabled: boolean;
    phoneNumberConfirmed: boolean;
    locked: boolean;
}