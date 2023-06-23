import {Component, Input} from '@angular/core';
import {NavItem} from "../../../../models/NavItem";
import {AuthItem} from "../../../../models/AuthItem";

@Component({
  selector: 'app-auth-items',
  templateUrl: './auth-items.component.html',
  styleUrls: ['./auth-items.component.scss']
})
export class AuthItemsComponent {
    @Input() isAuthenticated!: boolean;
    @Input() authItems!: AuthItem[]
}
