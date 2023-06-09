import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {GetGameResponse} from "../../models/GetGameResponse";
import {GetGameBriefResponse} from "../../models/GetGameBriefResponse";

@Component({
  selector: 'app-game-list-management',
  templateUrl: './game-list-management.component.html',
  styleUrls: ['./game-list-management.component.scss']
})
export class GameListManagementComponent{
    @Input() paginatedGames!: PaginationResult<GetGameBriefResponse>;
    @Input() userRole!: string | null;
    @Output() delete: EventEmitter<GetGameBriefResponse> = new EventEmitter<GetGameBriefResponse>();

    onDelete(game: GetGameBriefResponse) {
        this.delete.emit(game);
    }
}
