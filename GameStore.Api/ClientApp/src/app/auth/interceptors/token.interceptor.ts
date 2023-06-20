import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {catchError, Observable, switchMap, throwError} from 'rxjs';
import {AuthService} from "../services/auth.service";
import {AuthResponse} from "../models/AuthResponse";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(
        public authService: AuthService,
        private router: Router
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = this.authService.getAccessToken();
        const refreshToken = this.authService.getRefreshToken();

        if (accessToken) {
            request = this.addAuthorizationHeader(request, accessToken);
        }

        return next.handle(request).pipe(
            catchError((error) => {
                if (error.status === 401 && refreshToken) {
                    return this.authService.refreshToken(refreshToken).pipe(
                        switchMap((response: AuthResponse) => {
                            const newAccessToken = response.AccessToken;
                            this.authService.saveTokens(response);
                            request = this.addAuthorizationHeader(request, newAccessToken);
                            return next.handle(request);
                        }),
                        catchError((refreshError) => {
                            const userObjectId = this.authService.decodeAccessToken()?.UserObjectId;
                            this.authService.logout(userObjectId!).subscribe(() =>{
                                this.router.navigate(['/login']);
                            });
                            return throwError(refreshError);
                        })
                    );
                } else {
                    return throwError(error);
                }
            })
        );
    }

    private addAuthorizationHeader(request: HttpRequest<any>, token: string): HttpRequest<any> {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }
}
