import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreatePublisherRequest} from "../../models/CreatePublisherRequest";
import {CreateGameRequest} from "../../../games/models/CreateGameRequest";
import {PublisherService} from "../../services/publisher.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {ErrorHandlerService} from "../../../core/services/error-handler.service";

@Component({
    selector: 'app-create-publisher-page',
    templateUrl: './create-publisher-page.component.html',
    styleUrls: ['./create-publisher-page.component.scss']
})
export class CreatePublisherPageComponent {
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
                this.router.navigate(['/publisher/list'])
            },
            error: (error) => {
                this.errorHandlerService.handleApiError(error);
            }
        });
    }
}
