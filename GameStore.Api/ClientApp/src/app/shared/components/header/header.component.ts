import {Component, OnInit} from '@angular/core';
import {GameService} from "../../../games/services/game.service";
import {Observable} from "rxjs";
import {AuthService} from "../../../auth/services/auth.service";
import {PublisherService} from "../../../publishers/services/publisher.service";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    gamesCount!: number;
    isAuthenticated$!: Observable<boolean>;
    userRole$!: Observable<string | null>;
    currentPublisherCompanyName$!: Observable<string>;

    constructor(
        private gameService: GameService,
        private authService: AuthService,
        private publisherService: PublisherService) { }

    ngOnInit(): void {
        this.userRole$ = this.authService.getUserRole();
        this.currentPublisherCompanyName$ = this.publisherService.getCurrentPublisherCompanyName();
        this.isAuthenticated$ = this.authService.isAuthenticated();
        this.getCount();
    }

    private getCount(): void {
        this.gameService.getCount().subscribe(count => {
            this.gamesCount = count;
        })
    }
}
