import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetGenreResponse} from "../../genres/models/GetGenreResponse";
import {PlatformType} from "../models/PlatformType";

@Injectable({
    providedIn: 'root'
})
export class PlatformTypeService {
    private baseUrl = '/api/platform-types/';

    constructor(private http: HttpClient) { }

    getAllPlatformTypes(): Observable<PlatformType[]> {
        return this.http.get<PlatformType[]>(`${this.baseUrl}/list`);
    }
}
