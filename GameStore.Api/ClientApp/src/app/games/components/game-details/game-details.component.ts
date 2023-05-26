import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {GetCommentResponse} from "../../models/GetCommentResponse";
import {Genre} from "../../models/Genre";
import {CommentNode} from "../../models/CommentNode";

@Component({
    selector: 'app-game-details',
    templateUrl: './game-details.component.html',
    styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
    @Input() game!: GetGameResponse;
    @Input() isBuyButtonDisabled: boolean = false
    @Output() downloadGame = new EventEmitter<File>();
    @Output() buyGame = new EventEmitter<GetGameResponse>();
    parentGenres!: Genre[]

    ngOnInit(): void {
        this.getParentGenres();
    }

    onDownload(): void {
        this.downloadGame.emit();
    }

    onBuy(): void {
        this.buyGame.emit(this.game);
    }

    private getParentGenres(): void {
        this.parentGenres = this.game.Genres.filter(genre => genre.ParentGenreId === null);
    }
}
