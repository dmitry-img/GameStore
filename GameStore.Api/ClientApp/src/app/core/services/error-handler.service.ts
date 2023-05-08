import {Injectable} from '@angular/core';
import {ToastrService} from "ngx-toastr";

@Injectable({
    providedIn: 'root'
})
export class ErrorHandlerService {

    constructor(private toaster: ToastrService) {
    }

    handleApiError(error: any): void {
        if (error.status === 400) {
            const errorArray = error.error.Message.split(',');
            errorArray.forEach((message: string) => {
                this.toaster.error(message);
            });
        } else if (error.status === 500) {
            this.toaster.error('An internal server error occurred. Please try again later.');
        } else {
            this.toaster.error('An unexpected error occurred. Please try again later.');
        }
    }
}
