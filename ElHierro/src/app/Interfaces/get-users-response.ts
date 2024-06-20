import { User } from "./user";

export interface GetUsersResponse {
    message : string,
    result : User[],
    success : boolean
}
