import {Component, Input} from '@angular/core';
import {NavItem} from "../../../models/NavItem";
import {AuthItem} from "../../../models/AuthItem";
import {MainNavItem} from "../../../models/MainNavItem";

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.scss']
})
export class MainNavComponent {
    @Input() mainItems!: MainNavItem[];
    @Input() isAuthenticated!: boolean;
    @Input() gamesCount!: number;
    authItems: AuthItem[] = [
        { text: 'Logout', link: '/logout', isAuthenticated: true },
        { text: 'Login', link: '/login', isAuthenticated: false },
        { text: 'Register', link: '/register', isAuthenticated: false }
    ];
}
