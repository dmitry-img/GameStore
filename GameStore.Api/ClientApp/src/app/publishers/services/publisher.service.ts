import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetGameResponse} from "../../games/models/GetGameResponse";
import {GetPublisherBriefResponse} from "../models/GetPublisherBriefResponse";
import {GetPublisherResponse} from "../models/GetPublisherResponse";
import {CreatePublisherRequest} from "../models/CreatePublisherRequest";

@Injectable({
    providedIn: 'root'
})
export class PublisherService {

    private baseUrl = '/api/publishers';

    constructor(private http: HttpClient) { }

    getAllPublishersBrief(): Observable<GetPublisherBriefResponse[]> {
        return this.http.get<GetPublisherBriefResponse[]>(`${this.baseUrl}/brief`)
    }

    getPublisherByCompanyName(companyName: string): Observable<GetPublisherResponse> {
        return this.http.get<GetPublisherResponse>(`${this.baseUrl}/${companyName}`)
    }

    createPublisher(newPublisher: CreatePublisherRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/create`, newPublisher)
    }
}
