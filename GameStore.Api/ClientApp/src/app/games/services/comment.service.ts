import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {GetCommentResponse} from "../models/GetCommentResponse";
import {CreateCommentRequest} from "../models/CreateCommentRequest";
import {BanRequest} from "../models/BanRequest";

@Injectable({
    providedIn: 'root'
})
export class CommentService {
    private baseUrl = '/api/comments';
    private newCommentSubject = new Subject<boolean>();
    private replySubject = new Subject<GetCommentResponse>();
    private quoteSubject = new Subject<GetCommentResponse>();

    constructor(private http: HttpClient) { }

    getCommentList(gameKey: string): Observable<GetCommentResponse[]> {
        return this.http.get<GetCommentResponse[]>(`${this.baseUrl}/${gameKey}`);
    }

    createComment(newComment: CreateCommentRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/create`, newComment);
    }

    ban(banRequest: BanRequest): Observable<void>{
        return this.http.post<void>(`${this.baseUrl}/ban`, banRequest);
    }

    deleteComment(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/delete/${id}`);
    }

    emitNewComment(value: boolean): void {
        this.newCommentSubject.next(value);
    }

    getEmittedNewComment$(): Observable<boolean> {
        return this.newCommentSubject.asObservable();
    }

    emitDeletedComment(value: boolean): void {
        this.newCommentSubject.next(value);
    }

    getEmittedDeleteComment$(): Observable<boolean> {
        return this.newCommentSubject.asObservable();
    }

    emitReply(parentComment: GetCommentResponse){
        this.replySubject.next(parentComment);
    }

    emitQuote(parentComment: GetCommentResponse){
        this.quoteSubject.next(parentComment);
    }

    getEmittedReply$(): Observable<GetCommentResponse> {
        return this.replySubject.asObservable();
    }

    getEmittedQuote$(): Observable<GetCommentResponse> {
        return this.quoteSubject.asObservable();
    }
}
