import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreatePublisherRequest} from "../../../core/models/CreatePublisherRequest";
import {CreateGameRequest} from "../../../core/models/CreateGameRequest";
import {PublisherService} from "../../../core/service/publisher.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-publisher-page',
  templateUrl: './create-publisher-page.component.html',
  styleUrls: ['./create-publisher-page.component.scss']
})
export class CreatePublisherPageComponent {
  constructor(private publisherService: PublisherService,
              private toaster: ToastrService,
              private router: Router) { }

  onPublisherCreated(newPublisher: CreatePublisherRequest) {
    this.publisherService.createPublisher(newPublisher).subscribe({
      next: () =>{
        this.toaster.success("The publisher has been created successfully!")
        this.router.navigate(['/'])
      },
      error: (error)=>{
        const errorArray = error.error.Message.split(',');
        errorArray.forEach((message: string) => {
          this.toaster.error(message);
        })
      }
    });
  }
}
