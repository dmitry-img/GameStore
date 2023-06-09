import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreatePublisherRequest} from "../../models/CreatePublisherRequest";
import {CreateGameRequest} from "../../../games/models/CreateGameRequest";
import {PublisherService} from "../../services/publisher.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
    selector: 'app-create-publisher-page',
    templateUrl: './create-publisher-page.component.html',
    styleUrls: ['./create-publisher-page.component.scss']
})
export class CreatePublisherPageComponent {
    createPublisherForm!: FormGroup;
    freePublisherUsernames!: string[];

    constructor(
        private fb: FormBuilder,
        private publisherService: PublisherService,
        private router: Router,
        private toaster: ToastrService,
    ) { }

    ngOnInit(): void {
        this.createPublisherForm = this.fb.group({
            CompanyName: ['', [Validators.required, Validators.maxLength(40)]],
            Description: ['', Validators.required],
            HomePage: ['', [Validators.required, Validators.pattern('https?://.+')]],
            Username: ['', Validators.required]
        });

        this.publisherService.getFreePublisherUsernames().subscribe((usernames: string[]) =>{
            this.freePublisherUsernames = usernames;
        });
    }

    onSubmit(): void {
        if (this.createPublisherForm.invalid) {
            return;
        }

        this.publisherService.createPublisher(this.createPublisherForm.value).subscribe(() => {
            this.toaster.success("The publisher has been created successfully!")
            this.router.navigate(['/publisher/list'])
        });
    }
}
