import {Component, Input, OnInit} from '@angular/core';
import {GetPublisherResponse} from "../../models/GetPublisherResponse";
import {PublisherService} from "../../services/publisher.service";
import {ActivatedRoute} from "@angular/router";
import {GetCommentResponse} from "../../../games/models/GetCommentResponse";

@Component({
    selector: 'app-publisher-details-page',
    templateUrl: './publisher-details-page.component.html',
    styleUrls: ['./publisher-details-page.component.scss']
})
export class PublisherDetailsPageComponent implements OnInit {
    publisher!: GetPublisherResponse;

    constructor(
        private publisherService: PublisherService,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.getPublisher();
    }

    private getPublisher(): void {
        this.route.params.subscribe(data => {
            this.publisherService.getPublisherByCompanyName(data['companyName'])
                .subscribe((publisher: GetPublisherResponse) => {
                        this.publisher = publisher;
                    }
                )
        });
    }
}
