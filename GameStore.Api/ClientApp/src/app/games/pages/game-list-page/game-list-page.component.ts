import { Component } from '@angular/core';
import {GetGameResponse} from "../../../core/models/GetGameResponse";
import {GameService} from "../../services/game.service";

@Component({
  selector: 'app-game-list-page',
  templateUrl: './game-list-page.component.html',
  styleUrls: ['./game-list-page.component.scss']
})
export class GameListPageComponent {
  games!: GetGameResponse[];

  constructor(private gameService: GameService) { }

  ngOnInit() {
    this.getGames();
  }

  getGames() {
    this.gameService.getGameList().subscribe({
      next: (games: GetGameResponse[]) => {
        this.games = games;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
