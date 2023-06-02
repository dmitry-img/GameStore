import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetUserResponse} from "../models/GetUserResponse";
import {CreateUserRequest} from "../models/CreateUserRequest";
import {UpdateUserRequest} from "../models/UpdateUserRequest";
import {PaginationRequest} from "../../shared/models/PaginationRequest";
import {PaginationResult} from "../../shared/models/PaginationResult";


@Injectable({
  providedIn: 'root'
})
export class UserService {
    private baseUrl = '/api/users';

    constructor(private http: HttpClient) { }

    getAllUsersWithPagination(paginationRequest: PaginationRequest): Observable<PaginationResult<GetUserResponse>>{
        let params = new HttpParams()
            .set('PageNumber', paginationRequest.PageNumber.toString())
            .set('PageSize', paginationRequest.PageSize.toString());

        return this.http.get<PaginationResult<GetUserResponse>>(`${this.baseUrl}/paginated-list`, { params });
    }

    getUserById(id: number): Observable<GetUserResponse>{
        return this.http.get<GetUserResponse>(`${this.baseUrl}/${id}`);
    }

    createUser(createUserRequest: CreateUserRequest) : Observable<void>{
        return this.http.post<void>(`${this.baseUrl}/create`, createUserRequest);
    }

    updateUser(objectId: string, updateUserRequest: UpdateUserRequest): Observable<void>{
        return this.http.put<void>(`${this.baseUrl}/update/${objectId}`,updateUserRequest);
    }

    deleteUser(objectId: string): Observable<void>{
        return this.http.delete<void>(`${this.baseUrl}/delete/${objectId}`)
    }
}
