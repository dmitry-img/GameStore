import {Component, EventEmitter, Input, Output} from '@angular/core';
import {GetRoleResponse} from "../../models/GetRoleResponse";
import {UpdateRoleRequest} from "../../models/UpdateRoleRequest";
import {PaginationResult} from "../../../shared/models/PaginationResult";

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent {
    @Input() paginatedRoles!: PaginationResult<GetRoleResponse>;
    @Output() create: EventEmitter<void> = new EventEmitter<void>();
    @Output() delete: EventEmitter<GetRoleResponse> = new EventEmitter<GetRoleResponse>();

    onDelete(role: GetRoleResponse) {
        this.delete.emit(role);
    }

    onCreate() {
        this.create.emit();
    }
}
