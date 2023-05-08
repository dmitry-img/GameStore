import {Component} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {GameService} from "../../../core/services/game.service";

@Component({
    selector: 'app-game-list-page',
    templateUrl: './game-list-page.component.html',
    styleUrls: ['./game-list-page.component.scss']
})
export class GameListPageComponent {
    games!: GetGameResponse[];

    constructor(private gameService: GameService) {
    }

    ngOnInit(): void {
        this.getGames();
    }

    private getGames(): void {
        this.gameService.getAllGames().subscribe((games: GetGameResponse[]) => {
            this.games = games;
        });
    }
}
