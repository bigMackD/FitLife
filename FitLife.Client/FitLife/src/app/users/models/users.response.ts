import { IBaseResponse } from 'src/app/shared/interfaces/base.response';
import { IPagingBaseResponse } from 'src/app/shared/interfaces/pagingBase.response';


export class UsersResponse implements IBaseResponse, IPagingBaseResponse {
    success: boolean;
    errors: string[];
    users: User[];
    count: number; 
}

export interface User{
    id: string,
    email: string,
    fullName: string
}