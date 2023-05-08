import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {Genre} from "../models/Genre";
import {HttpClient} from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class GenreService {
    private baseUrl = '/api/genres/';

    constructor(private http: HttpClient) {
    }

    getAllGenres(): Observable<Genre[]> {
        return this.http.get<Genre[]>(this.baseUrl);
    }
}
