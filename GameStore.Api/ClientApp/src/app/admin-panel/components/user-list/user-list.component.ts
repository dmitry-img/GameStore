import {Component, EventEmitter, Input, Output} from '@angular/core';
import {GetUserResponse} from "../../models/GetUserResponse";
import {GetRoleResponse} from "../../models/GetRoleResponse";
import {PaginationResult} from "../../../shared/models/PaginationResult";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
    @Input() paginatedUsers!: PaginationResult<GetUserResponse>;
    @Output() create: EventEmitter<void> = new EventEmitter<void>();
    @Output() modify: EventEmitter<GetUserResponse> = new EventEmitter<GetUserResponse>();
    @Output() delete: EventEmitter<GetUserResponse> = new EventEmitter<GetUserResponse>();

    onModify(user: GetUserResponse) {
        this.modify.emit(user);
    }

    onDelete(user: GetUserResponse) {
        this.delete.emit(user);
    }

    onCreate() {
        this.create.emit();
    }
}
