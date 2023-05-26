import {Component, OnInit} from '@angular/core';
import {GameService} from "../../../games/services/game.service";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    gamesCount!: number;

    constructor(private gameService: GameService) { }

    ngOnInit(): void {
        this.getCount();
    }

    private getCount(): void {
        this.gameService.getCount().subscribe(count => {
            this.gamesCount = count;
        })
    }
}
