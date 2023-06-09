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
    private userRoleSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(this.getUserRoleFromToken());

    constructor(private http: HttpClient) {
    }

    login(loginRequest: LoginRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/login`, loginRequest).pipe(
            tap((authResponse: AuthResponse) => {
                this.saveTokens(authResponse);
                this.isAuthenticatedSubject.next(this.checkTokenExistence());
                this.userRoleSubject.next(this.getUserRoleFromToken());
            })
        );
    }

    refreshToken(refreshToken: string): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/refresh?refreshToken=${refreshToken}`, {});
    }

    logout(userObjectId: string): Observable<void>{
        localStorage.clear();
        this.isAuthenticatedSubject.next(this.checkTokenExistence());
        this.userRoleSubject.next(null);
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
            Username: decodedToken.unique_name,
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

    getUserRole(): Observable<string | null> {
        return this.userRoleSubject.asObservable();
    }

    private getUserRoleFromToken(): string | null {
        const decodedToken = this.decodeAccessToken();
        return decodedToken?.Role ?? null;
    }

    private checkTokenExistence(): boolean {
        return !!this.getAccessToken();
    }
}
