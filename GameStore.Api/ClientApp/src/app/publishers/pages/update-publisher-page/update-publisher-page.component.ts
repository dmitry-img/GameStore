import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PublisherService} from "../../services/publisher.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {ErrorHandlerService} from "../../../core/services/error-handler.service";
import {GetPublisherResponse} from "../../models/GetPublisherResponse";

@Component({
  selector: 'app-update-publisher-page',
  templateUrl: './update-publisher-page.component.html',
  styleUrls: ['./update-publisher-page.component.scss']
})
export class UpdatePublisherPageComponent {
    updatePublisherForm!: FormGroup;
    publisher!: GetPublisherResponse;

    constructor(
        private fb: FormBuilder,
        private publisherService: PublisherService,
        private router: Router,
        private route: ActivatedRoute,
        private toaster: ToastrService,
        private errorHandlerService: ErrorHandlerService
    ) { }

    ngOnInit(): void {
        this.updatePublisherForm = this.fb.group({
            CompanyName: ['', [Validators.required, Validators.maxLength(40)]],
            Description: ['', Validators.required],
            HomePage: ['', [Validators.required, Validators.pattern('https?://.+')]]
        });

        this.getPublisher();
    }

    getPublisher(){
        this.route.params.subscribe(data =>{
            this.publisherService.getPublisherByCompanyName(data['companyName'])
                .subscribe((publisher:GetPublisherResponse) => {
                    this.publisher = publisher;
                    this.updatePublisherForm.setValue(publisher);
                });
        });
    }

    onSubmit(): void {
        if (this.updatePublisherForm.invalid) {
            return;
        }

        this.publisherService.updatePublisher(this.publisher.CompanyName, this.updatePublisherForm.value).subscribe({
            next: () => {
                this.toaster.success("The publisher has been created successfully!")
                this.router.navigate(['/publisher/list'])
            },
            error: (error: any) => {
                this.errorHandlerService.handleApiError(error);
            }
        });
    }
}
