import {Component, EventEmitter, Input, Output} from '@angular/core';
import {GetRoleResponse} from "../../../admin-panel/models/GetRoleResponse";
import {GetGenreResponse} from "../../models/GetGenreResponse";
import {PaginationResult} from "../../../shared/models/PaginationResult";

@Component({
  selector: 'app-genre-list',
  templateUrl: './genre-list.component.html',
  styleUrls: ['./genre-list.component.scss']
})
export class GenreListComponent {
    @Input() paginatedGenres!: PaginationResult<GetGenreResponse>;
    @Output() create: EventEmitter<void> = new EventEmitter<void>();
    @Output() modify: EventEmitter<GetGenreResponse> = new EventEmitter<GetGenreResponse>();
    @Output() delete: EventEmitter<GetGenreResponse> = new EventEmitter<GetGenreResponse>();

    onModify(genre: GetGenreResponse) {
        this.modify.emit(genre);
    }

    onDelete(genre: GetGenreResponse) {
        this.delete.emit(genre);
    }

    onCreate() {
        this.create.emit();
    }
}
