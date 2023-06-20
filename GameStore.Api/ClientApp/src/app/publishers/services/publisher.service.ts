import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetGameResponse} from "../../games/models/GetGameResponse";
import {GetPublisherBriefResponse} from "../models/GetPublisherBriefResponse";
import {GetPublisherResponse} from "../models/GetPublisherResponse";
import {CreatePublisherRequest} from "../models/CreatePublisherRequest";
import {PaginationRequest} from "../../shared/models/PaginationRequest";
import {PaginationResult} from "../../shared/models/PaginationResult";
import {UpdatePublisherRequest} from "../models/UpdatePublisherRequest";
import {observableToBeFn} from "rxjs/internal/testing/TestScheduler";

@Injectable({
    providedIn: 'root'
})
export class PublisherService {

    private baseUrl = '/api/publishers';

    constructor(private http: HttpClient) { }

    getAllPublishersBrief(): Observable<GetPublisherBriefResponse[]> {
        return this.http.get<GetPublisherBriefResponse[]>(`${this.baseUrl}/brief`)
    }

    getAllPublishersBriefWithPagination(paginationRequest: PaginationRequest): Observable<PaginationResult<GetPublisherBriefResponse>> {
        let params = new HttpParams()
            .set('PageNumber', paginationRequest.PageNumber.toString())
            .set('PageSize', paginationRequest.PageSize.toString());

        return this.http.get<PaginationResult<GetPublisherBriefResponse>>(`${this.baseUrl}/paginated-list`, { params })
    }

    getPublisherByCompanyName(companyName: string): Observable<GetPublisherResponse> {
        return this.http.get<GetPublisherResponse>(`${this.baseUrl}/${companyName}`)
    }

    createPublisher(newPublisher: CreatePublisherRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/create`, newPublisher)
    }

    updatePublisher(companyName: string, publisher: UpdatePublisherRequest): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/update/${companyName}`, publisher);
    }

    deletePublisher(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/delete/${id}`);
    }

    isGameAssociatedWithPublisher(gameKey: string): Observable<boolean>{
        return this.http.get<boolean>(`${this.baseUrl}/${gameKey}/is-game-associated-with-publisher`);
    }

    isUserAssociatedWithPublisher(gameKey: string): Observable<boolean>{
        return this.http.get<boolean>(`${this.baseUrl}/${gameKey}/is-user-associated-with-publisher`);
    }

    getCurrentPublisherCompanyName(): Observable<string>{
        return this.http.get<string>(`${this.baseUrl}/current`);
    }

    getFreePublisherUsernames(): Observable<string[]>{
        return this.http.get<string[]>(`${this.baseUrl}/free`);
    }
}
