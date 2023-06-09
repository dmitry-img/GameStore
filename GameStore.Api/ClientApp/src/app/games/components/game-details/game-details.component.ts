import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {GetCommentResponse} from "../../models/GetCommentResponse";
import {GetGenreResponse} from "../../../genres/models/GetGenreResponse";
import {CommentNode} from "../../models/CommentNode";

@Component({
    selector: 'app-game-details',
    templateUrl: './game-details.component.html',
    styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
    @Input() game!: GetGameResponse;
    parentGenres!: GetGenreResponse[]

    ngOnInit(): void {
        this.getParentGenres();
    }

    private getParentGenres(): void {
        this.parentGenres = this.game.Genres.filter(genre => genre.ParentGenreId === null);
    }
}
