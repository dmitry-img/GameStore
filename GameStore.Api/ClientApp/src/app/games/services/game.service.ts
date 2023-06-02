import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {GetGameResponse} from "../models/GetGameResponse";
import {Observable} from "rxjs";
import {CreateGameRequest} from "../../games/models/CreateGameRequest";
import {UpdateGameRequest} from "../../games/models/UpdateGameRequest";
import {FilterGameRequest} from "../../games/models/FilterGameRequest";
import {PaginationResult} from "../../shared/models/PaginationResult";
import {PaginationRequest} from "../../shared/models/PaginationRequest";

@Injectable({
    providedIn: 'root'
})
export class GameService {
    private baseUrl = '/api/games';

    constructor(private http: HttpClient) { }

    getGames(filter: FilterGameRequest): Observable<PaginationResult<GetGameResponse>> {
        let params = new HttpParams();

        Object.keys(filter).forEach((key) => {
            const value = filter[key as keyof FilterGameRequest];
            if (value !== null) {
                if (Array.isArray(value)) {
                    value.forEach(item => {
                        params = params.append(key, item.toString());
                    });
                } else if (value instanceof Object && key === 'Pagination') {
                    Object.keys(value).forEach(subKey => {
                        params = params.set(`${key}.${subKey}`, value[subKey as keyof PaginationRequest].toString());
                    });
                } else {
                    params = params.set(key, value.toString());
                }
            }
        });

        return this.http.get<PaginationResult<GetGameResponse>>(`${this.baseUrl}/list`, { params });
    }

    getGameByKey(key: string): Observable<GetGameResponse> {
        return this.http.get<GetGameResponse>(`${this.baseUrl}/${key}`)
    }

    createGame(newGame: CreateGameRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/create`, newGame)
    }

    updateGame(key: string, game: UpdateGameRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/update/${key}`, game)
    }

    downloadGame(key: string, name: string): void {
        this.http.get(`${this.baseUrl}/download/${key}`, {responseType: 'blob'})
            .subscribe((response: any) => {
                const blob = new Blob([response], {type: response.type});
                const downloadUrl = URL.createObjectURL(blob);
                const link = document.createElement('a');
                link.href = downloadUrl;
                link.download = name;
                link.click();
            });
    }

    getCount(): Observable<number> {
        return this.http.get<number>(`${this.baseUrl}/count`)
    }
}
