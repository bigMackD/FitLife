import { IBasePagingRequest } from "src/app/shared/interfaces/pagingBase.request";

export class MealCategoriesRequest implements IBasePagingRequest  {
    sortDirection: string;
    pageIndex: number;
    pageSize: number;

}