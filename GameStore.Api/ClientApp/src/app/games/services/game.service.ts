import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GetGameResponse} from "../../core/models/GetGameResponse";
import {Observable} from "rxjs";
import {CreateGameRequest} from "../../core/models/CreateGameRequest";
import {UpdateGameRequest} from "../../core/models/UpdateGameRequest";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private baseUrl = '/api/games/';
  constructor(private http: HttpClient) { }

  getGameList() : Observable<GetGameResponse[]>{
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
}
