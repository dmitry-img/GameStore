import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {finalize, Observable} from 'rxjs';
import {NgxSpinnerService} from "ngx-spinner";

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

    constructor(public spinner: NgxSpinnerService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.spinner.show();

        return next.handle(req).pipe(
            finalize(() => {
                this.spinner.hide();
            })
        );
    }
}

