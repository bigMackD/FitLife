import { IBaseResponse } from 'src/app/shared/interfaces/base.response';

export class  UserProfileResponse implements IBaseResponse{
    success: boolean;
    errors: string[];
    fullName: string;
}