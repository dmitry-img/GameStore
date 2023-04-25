import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreatePublisherRequest} from "../../../core/models/CreatePublisherRequest";

@Component({
  selector: 'app-create-publisher',
  templateUrl: './create-publisher.component.html',
  styleUrls: ['./create-publisher.component.scss']
})
export class CreatePublisherComponent {
  createPublisherForm!: FormGroup;
  @Output() publisherCreated = new EventEmitter<CreatePublisherRequest>();

  constructor(private fb: FormBuilder) {
    this.createForm();
  }

  createForm() {
    this.createPublisherForm = this.fb.group({
      CompanyName: ['', [Validators.required, Validators.maxLength(40)]],
      Description: ['', Validators.required],
      HomePage: ['', Validators.required]
    });
  }

  onSubmit() {
    const publisherRequest: CreatePublisherRequest = this.createPublisherForm.value;
    this.publisherCreated.next(publisherRequest);
  }
}
