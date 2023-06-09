import {Component, Input, OnInit} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetGameBriefResponse} from "../../models/GetGameBriefResponse";

@Component({
    selector: 'app-game-list',
    templateUrl: './game-list.component.html',
    styleUrls: ['./game-list.component.scss']
})
export class GameListComponent {
    @Input() paginatedGames!: PaginationResult<GetGameBriefResponse>;
}
