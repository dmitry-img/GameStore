import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {GetCommentResponse} from "../models/GetCommentResponse";
import {CreateCommentRequest} from "../models/CreateCommentRequest";

@Injectable({
    providedIn: 'root'
})
export class CommentService {
    private baseUrl = '/api/comments';
    private newCommentSubject = new Subject<boolean>();

    constructor(private http: HttpClient) { }

    getCommentList(gameKey: string): Observable<GetCommentResponse[]> {
        return this.http.get<GetCommentResponse[]>(`${this.baseUrl}/${gameKey}`);
    }

    createComment(newComment: CreateCommentRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/create`, newComment);
    }

    emitComment(value: boolean): void {
        this.newCommentSubject.next(value);
    }

    getEmittedComment$(): Observable<boolean> {
        return this.newCommentSubject.asObservable();
    }
}
