import {GetRoleResponse} from "./GetRoleResponse";

export interface GetUserResponse{
    ObjectId: string
    Email: string
    Username: string
    Role: GetRoleResponse
}
