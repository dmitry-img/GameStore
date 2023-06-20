import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetRoleResponse} from "../models/GetRoleResponse";
import {CreateRoleRequest} from "../models/CreateRoleRequest";
import {UpdateRoleRequest} from "../models/UpdateRoleRequest";
import {PaginationRequest} from "../../shared/models/PaginationRequest";
import {PaginationResult} from "../../shared/models/PaginationResult";

@Injectable({
  providedIn: 'root'
})
export class RoleService {
    private baseUrl = '/api/roles';

    constructor(private http: HttpClient) { }

    getAllRolesWithPagination(paginationRequest: PaginationRequest): Observable<PaginationResult<GetRoleResponse>>{
        let params = new HttpParams()
            .set('PageNumber', paginationRequest.PageNumber.toString())
            .set('PageSize', paginationRequest.PageSize.toString());

        return this.http.get<PaginationResult<GetRoleResponse>>(`${this.baseUrl}/paginated-list`, { params });
    }

    getAllRoles(): Observable<GetRoleResponse[]>{
        return this.http.get<GetRoleResponse[]>(`${this.baseUrl}/list`);
    }

    getRoleById(id: number): Observable<GetRoleResponse>{
        return this.http.get<GetRoleResponse>(`${this.baseUrl}/${id}`);
    }

    createRole(createRoleRequest: CreateRoleRequest) : Observable<void>{
        return this.http.post<void>(`${this.baseUrl}/create`, createRoleRequest);
    }

    updateRole(id: number, updateRoleRequest: UpdateRoleRequest): Observable<void>{
        return this.http.put<void>(`${this.baseUrl}/update/${id}`,updateRoleRequest);
    }

    deleteRole(id: number): Observable<void>{
        return this.http.delete<void>(`${this.baseUrl}/delete/${id}`)
    }
}
