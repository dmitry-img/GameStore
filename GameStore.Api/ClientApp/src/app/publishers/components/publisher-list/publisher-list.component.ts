import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetGenreResponse} from "../../../genres/models/GetGenreResponse";
import {GetPublisherBriefResponse} from "../../models/GetPublisherBriefResponse";

@Component({
  selector: 'app-publisher-list',
  templateUrl: './publisher-list.component.html',
  styleUrls: ['./publisher-list.component.scss']
})
export class PublisherListComponent {
    @Input() paginatedPublishers!: PaginationResult<GetPublisherBriefResponse>;
    @Output() delete: EventEmitter<GetPublisherBriefResponse> = new EventEmitter<GetPublisherBriefResponse>();

    onDelete(genre: GetPublisherBriefResponse) {
        this.delete.emit(genre);
    }
}
