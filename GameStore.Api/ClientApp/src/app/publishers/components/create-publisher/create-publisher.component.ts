import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreatePublisherRequest} from "../../models/CreatePublisherRequest";
import {PublisherService} from "../../../core/services/publisher.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {ErrorHandlerService} from "../../../core/services/error-handler.service";

@Component({
    selector: 'app-create-publisher',
    templateUrl: './create-publisher.component.html',
    styleUrls: ['./create-publisher.component.scss']
})
export class CreatePublisherComponent implements OnInit {
    createPublisherForm!: FormGroup;

    constructor(
        private fb: FormBuilder,
        private publisherService: PublisherService,
        private router: Router,
        private toaster: ToastrService,
        private errorHandlerService: ErrorHandlerService
    ) { }

    ngOnInit(): void {
        this.createPublisherForm = this.fb.group({
            CompanyName: ['', [Validators.required, Validators.maxLength(40)]],
            Description: ['', Validators.required],
            HomePage: ['', [Validators.required, Validators.pattern('https?://.+')]]
        });
    }

    onSubmit(): void {
        if (this.createPublisherForm.invalid) {
            return;
        }

        this.publisherService.createPublisher(this.createPublisherForm.value).subscribe({
            next: () => {
                this.toaster.success("The publisher has been created successfully!")
                this.router.navigate(['/'])
            },
            error: (error) => {
                this.errorHandlerService.handleApiError(error);
            }
        });
    }
}
