import {Component, OnInit} from '@angular/core';
import {GameService} from "../../../games/services/game.service";
import {filter, Observable} from "rxjs";
import {AuthService} from "../../../auth/services/auth.service";
import {PublisherService} from "../../../publishers/services/publisher.service";
import {NavItem} from "../../models/NavItem";
import {MainNavItem} from "../../models/MainNavItem";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    gamesCount!: number;
    isAuthenticated$!: Observable<boolean>;
    currentPublisherCompanyName!: string;
    topNavItems!: NavItem[];
    topNavContacts!: NavItem[];
    mainNavItems!: MainNavItem[];

    constructor(
        private gameService: GameService,
        private authService: AuthService,
        private publisherService: PublisherService) { }

    ngOnInit(): void {
        this.isAuthenticated$ = this.authService.isAuthenticated$;

        this.isAuthenticated$.subscribe(() => {
            if(this.authService.decodeAccessToken()?.Role == 'Publisher'){
                this.publisherService.getCurrentPublisherCompanyName().subscribe((name: string) =>{
                    this.currentPublisherCompanyName = name;
                    this.getItems();
                });
            } else {
                this.getItems();
            }
        });

        this.getCount();
    }

    private getItems(){
        this.topNavItems = [
            { text: 'GameStore Home', link: '#' },
            { text: 'About GameStore', link: '#' },
            { text: 'Careers', link: '#' },
            { text: 'Newsroom', link: '#' }
        ];
        this.topNavContacts = [
            { text: 'Support', link: '#' },
            { text: 'Contact Us', link: '#' },
            { text: 'Online support', link: '#', },
            { text: 'Security Information', link: '#', }
        ];
        this.mainNavItems = [
            { text: 'Shopping Cart', link: '/shopping-cart', roles: ['User'] },
            { text: 'Admin Panel', link: 'admin-panel', roles: ['Administrator'] },
            { text: 'Games', link: 'game/list/management', roles: ['Manager', 'Moderator', 'Publisher'] },
            { text: 'Profile', link: `publisher/update/${this.currentPublisherCompanyName}`, roles: ['Publisher'] },
            { text: 'Publishers', link: '/publisher/list', roles: ['Manager'] },
            { text: 'Genres', link: '/genre/list', roles: ['Manager'] },
            { text: 'Orders', link: '/order/list', roles: ['Manager'] }
        ];
    }

    private getCount(): void {
        this.gameService.getCount().subscribe(count => {
            this.gamesCount = count;
        })
    }
}
