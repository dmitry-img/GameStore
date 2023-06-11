import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {ToastrService} from "ngx-toastr";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

    constructor(private toaster: ToastrService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        const errorArray = error.error.split(',');
                        errorArray.forEach((message: string) => {
                            this.toaster.error(message);
                        });
                    } else if (error.status === 500 || error.status == 404) {
                        this.toaster.error(error.error);
                    } else {
                        this.toaster.error('An unexpected error occurred. Please try again later.');
                    }
                    return throwError(() => error);
                })
            );
    }
}
