import { Injectable } from '@angular/core';
import {CreateCommentRequest} from "../../games/models/CreateCommentRequest";
import {BehaviorSubject, Observable, of, tap} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {LoginRequest} from "../models/LoginRequest";
import {RegistrationRequest} from "../models/RegistrationRequest";
import {AuthResponse} from "../models/AuthResponse";
import jwt_decode from 'jwt-decode';
import {DecodedAccessToken} from "../models/DecodedAccessToken";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUrl = '/api/auth';
    private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.checkTokenExistence());

    constructor(private http: HttpClient) {
    }

    private checkTokenExistence(): boolean {
        return !!this.getAccessToken();
    }

    login(loginRequest: LoginRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/login`, loginRequest).pipe(
            tap((authResponse: AuthResponse) => {
                this.saveTokens(authResponse);
                this.isAuthenticatedSubject.next(this.checkTokenExistence());
            })
        );
    }

    refreshToken(refreshToken: string): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/refresh?refreshToken=${refreshToken}`, {});
    }

    logout(userObjectId: string): Observable<void>{
        localStorage.clear();
        this.isAuthenticatedSubject.next(this.checkTokenExistence());
        return this.http.post<void>(`${this.baseUrl}/logout?userObjectId=${userObjectId}`, {});
    }

    register(registrationRequest: RegistrationRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/register`, registrationRequest).pipe(
            tap(() => {
                this.isAuthenticatedSubject.next(this.checkTokenExistence());
            })
        );
    }

    saveTokens(authResponse: AuthResponse): void{
        localStorage.setItem('accessToken', authResponse.AccessToken);
        localStorage.setItem('refreshToken', authResponse.RefreshToken);
    }

    decodeAccessToken(): DecodedAccessToken | null {
        const token = this.getAccessToken();
        if (!token) {
            return null;
        }
        const decodedToken: any = jwt_decode(token);
        return {
            Username: decodedToken.sub,
            UserObjectId: decodedToken.nameid,
            Role: decodedToken.role
        };
    }

    getAccessToken(): string | null {
        return localStorage.getItem('accessToken');
    }

    getRefreshToken(): string | null {
        return localStorage.getItem('refreshToken');
    }

    isAuthenticated(): Observable<boolean> {
        return this.isAuthenticatedSubject.asObservable();
    }

    getUserRole(): string | null {
        const decodedToken = this.decodeAccessToken();
        return decodedToken?.Role ?? null;
    }
}
