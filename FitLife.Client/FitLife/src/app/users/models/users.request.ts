import { IBasePagingRequest } from 'src/app/shared/interfaces/pagingBase.request';

export class UsersRequest implements IBasePagingRequest{
    sortDirection: string;
    pageIndex: number;
    pageSize: number;
}