import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpParams} from "@angular/common/http";
import {CreateGenreRequest} from "../models/CreateGenreRequest";
import {UpdateGenreRequest} from "../models/UpdateGenreRequest";
import {GetGenreResponse} from "../models/GetGenreResponse";
import {PaginationRequest} from "../../shared/models/PaginationRequest";
import {PaginationResult} from "../../shared/models/PaginationResult";
import {GetUserResponse} from "../../admin-panel/models/GetUserResponse";

@Injectable({
  providedIn: 'root'
})
export class GenreService {

    private baseUrl = '/api/genres';

    constructor(private http: HttpClient) { }

    getAllGenres(): Observable<GetGenreResponse[]> {
        return this.http.get<GetGenreResponse[]>(`${this.baseUrl}/list`);
    }

    getAllGenresWithPagination(paginationRequest: PaginationRequest): Observable<PaginationResult<GetGenreResponse>> {
        let params = new HttpParams()
            .set('PageNumber', paginationRequest.PageNumber.toString())
            .set('PageSize', paginationRequest.PageSize.toString());

        return this.http.get<PaginationResult<GetGenreResponse>>(`${this.baseUrl}/paginated-list`, { params });
    }

    getGenreById(id: number): Observable<GetGenreResponse>{
        return this.http.get<GetGenreResponse>(`${this.baseUrl}/${id}`);
    }

    createGenre(createGenreRequest: CreateGenreRequest) : Observable<void>{
        return this.http.post<void>(`${this.baseUrl}/create`, createGenreRequest);
    }

    updateGenre(id: number, updateGenreRequest: UpdateGenreRequest): Observable<void>{
        return this.http.put<void>(`${this.baseUrl}/update/${id}`,updateGenreRequest);
    }

    deleteGenre(id: number): Observable<void>{
        return this.http.delete<void>(`${this.baseUrl}/delete/${id}`)
    }
}
