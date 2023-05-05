import {Component, Input} from '@angular/core';
import {GetPublisherResponse} from "../../models/GetPublisherResponse";

@Component({
  selector: 'app-publisher-details',
  templateUrl: './publisher-details.component.html',
  styleUrls: ['./publisher-details.component.scss']
})
export class PublisherDetailsComponent {
  @Input() publisher!: GetPublisherResponse;
}
