import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PublisherService} from "../../services/publisher.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {GetPublisherResponse} from "../../models/GetPublisherResponse";
import {AuthService} from "../../../auth/services/auth.service";
import {iif, of, switchMap, tap} from "rxjs";

@Component({
  selector: 'app-update-publisher-page',
  templateUrl: './update-publisher-page.component.html',
  styleUrls: ['./update-publisher-page.component.scss']
})
export class UpdatePublisherPageComponent {
    updatePublisherForm!: FormGroup;
    publisher!: GetPublisherResponse;
    freePublisherUsernames!: string[];
    userRole!: string | null;

    constructor(
        private fb: FormBuilder,
        private publisherService: PublisherService,
        private authService: AuthService,
        private route: ActivatedRoute,
        private router: Router,
        private toaster: ToastrService,
    ) { }

    ngOnInit(): void {
        this.updatePublisherForm = this.fb.group({
            CompanyName: ['', [Validators.required, Validators.maxLength(40)]],
            Description: ['', Validators.required],
            HomePage: ['', [Validators.required, Validators.pattern('https?://.+')]],
            Username: ['', Validators.required]
        });

        this.userRole = this.authService.decodeAccessToken()!.Role;

        this.getPublisher();
    }

    getPublisher() {
        this.route.params.pipe(
            switchMap((params) =>
                this.publisherService.getPublisherByCompanyName(params['companyName']).pipe(
                    tap((publisher: GetPublisherResponse) => {
                        this.publisher = publisher;
                    }),
                    switchMap(() =>
                        iif(() => this.userRole === 'Manager',
                            this.publisherService.getFreePublisherUsernames(),
                            of([])
                        )
                    )
                )
            )
        ).subscribe({
            next: (usernames: string[]) => {
                this.freePublisherUsernames = usernames;
                this.freePublisherUsernames.push(this.publisher.Username);

                this.updatePublisherForm.setValue(this.publisher);
            },
            error: () => {
                this.router.navigate(['/'])
            }
        });
    }


    onSubmit(): void {
        if (this.updatePublisherForm.invalid) {
            return;
        }

        this.publisherService.updatePublisher(this.publisher.CompanyName, this.updatePublisherForm.value).subscribe(() => {
            this.toaster.success("The publisher has been updated successfully!");
        });
    }
}
