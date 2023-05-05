import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GetGameResponse} from "../../games/models/GetGameResponse";
import {Observable} from "rxjs";
import {CreateGameRequest} from "../../games/models/CreateGameRequest";
import {UpdateGameRequest} from "../../games/models/UpdateGameRequest";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private baseUrl = '/api/games';
  constructor(private http: HttpClient) { }

  getAllGames() : Observable<GetGameResponse[]>{
    return this.http.get<GetGameResponse[]>(this.baseUrl)
  }

  getGameByKey(key: string) : Observable<GetGameResponse>{
    return this.http.get<GetGameResponse>(`${this.baseUrl}/${key}`)
  }

  createGame(newGame: CreateGameRequest) : Observable<void>{
    return this.http.post<void>(`${this.baseUrl}/create`, newGame)
  }

  updateGame(key: string, game: UpdateGameRequest) : Observable<void>{
    return this.http.post<void>(`${this.baseUrl}/update/${key}`, game)
  }

  downloadGame(key: string, name: string): void{
    this.http.get(`${this.baseUrl}/download/${key}`, { responseType: 'blob' })
      .subscribe((response: any) => {
        const blob = new Blob([response], { type: response.type });
        const downloadUrl = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadUrl;
        link.download = name;
        link.click();
      });
  }

  getCount() : Observable<number>{
    return this.http.get<number>(`${this.baseUrl}/count`)
  }
}
