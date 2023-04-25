import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GetGameResponse} from "../models/GetGameResponse";
import {Observable} from "rxjs";
import {CreateGameRequest} from "../models/CreateGameRequest";
import {UpdateGameRequest} from "../models/UpdateGameRequest";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private baseUrl = '/api/games/';
  constructor(private http: HttpClient) { }

  getAllGames() : Observable<GetGameResponse[]>{
    return this.http.get<GetGameResponse[]>(this.baseUrl)
  }

  getGameByKey(key: string) : Observable<GetGameResponse>{
    return this.http.get<GetGameResponse>(`${this.baseUrl}/${key}`)
  }

  createGame(newGame: CreateGameRequest) : Observable<void>{
    return this.http.post<void>(this.baseUrl + 'create', newGame)
  }

  updateGame(key: string, game: UpdateGameRequest) : Observable<void>{
    return this.http.post<void>(`${this.baseUrl}/update/${key}`, game)
  }

  downloadGame(key: string, name: string){
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
