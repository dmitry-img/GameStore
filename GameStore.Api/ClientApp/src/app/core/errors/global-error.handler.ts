import {ErrorHandler, Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {HttpErrorResponse} from "@angular/common/http";

@Injectable()
export class GlobalErrorHandlerService extends ErrorHandler {

    constructor(private router: Router, private toaster: ToastrService) {
        super();
    }

    override handleError(error: Error | HttpErrorResponse) {

        let errorMessage = '';

        if (error instanceof HttpErrorResponse) {
            if (!navigator.onLine) {
                errorMessage = 'No Internet Connection';
            } else {
                errorMessage = `Error Code: ${error.status},  Message: ${error.message}`;
            }
        } else {
            errorMessage = error.message ? error.message : error.toString();
        }

        this.toaster.error(errorMessage, 'Something went wrong...');

        this.router.navigate(['/error']);
    }
}
