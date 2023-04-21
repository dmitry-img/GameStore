import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetCommentResponse} from "../../core/models/GetCommentResponse";
import {CreateCommentRequest} from "../../core/models/CreateCommentRequest";

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private baseUrl = '/api/comments/';
  constructor(private http: HttpClient) { }

  getCommentList(gameKey: string) : Observable<GetCommentResponse[]>{
    return this.http.get<GetCommentResponse[]>(`${this.baseUrl}/${gameKey}`);
  }

  createComment(newComment: CreateCommentRequest) : Observable<void>{
    return this.http.post<void>(`${this.baseUrl}/create`, newComment);
  }
}
