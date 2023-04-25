import {Component, Input, OnInit} from '@angular/core';
import {GetPublisherResponse} from "../../../core/models/GetPublisherResponse";
import {PublisherService} from "../../../core/service/publisher.service";
import {ActivatedRoute} from "@angular/router";
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";

@Component({
  selector: 'app-publisher-details-page',
  templateUrl: './publisher-details-page.component.html',
  styleUrls: ['./publisher-details-page.component.scss']
})
export class PublisherDetailsPageComponent implements OnInit{
  publisher!: GetPublisherResponse;
  constructor(private publisherService: PublisherService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(data =>{
      this.publisherService.getPublisherByCompanyName(data['companyName']).subscribe({
        next: (publisher: GetPublisherResponse) =>{
          this.publisher = publisher;
        },
        error: (error) =>{
          console.log(error);
        }
      })
    })


  }
}
