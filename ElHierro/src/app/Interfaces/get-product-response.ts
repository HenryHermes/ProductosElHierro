import { Product } from "./product";

export interface GetProductResponse {
    message : string,
    result : Product[],
    success : boolean
}
