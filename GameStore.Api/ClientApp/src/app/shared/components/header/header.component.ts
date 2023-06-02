import {Component, OnInit} from '@angular/core';
import {GameService} from "../../../games/services/game.service";
import {Observable} from "rxjs";
import {AuthService} from "../../../auth/services/auth.service";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    gamesCount!: number;
    isAuthenticated$!: Observable<boolean>;

    constructor(private gameService: GameService, private authService: AuthService) { }

    ngOnInit(): void {
        this.isAuthenticated$ = this.authService.isAuthenticated();
        this.getCount();
    }

    private getCount(): void {
        this.gameService.getCount().subscribe(count => {
            this.gamesCount = count;
        })
    }
}
