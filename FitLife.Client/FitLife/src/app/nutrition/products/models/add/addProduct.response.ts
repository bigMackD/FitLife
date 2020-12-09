import { IBaseResponse } from 'src/app/shared/interfaces/base.response';

export class AddProductResponse implements IBaseResponse {
    success: boolean;
    errors: string[];
}