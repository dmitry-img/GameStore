import {Component, Input} from '@angular/core';
import {AuthItem} from "../../../../models/AuthItem";
import {faCheck, IconDefinition} from "@fortawesome/free-solid-svg-icons";


@Component({
  selector: 'app-auth-items',
  templateUrl: './auth-items.component.html',
  styleUrls: ['./auth-items.component.scss']
})
export class AuthItemsComponent {
    @Input() isAuthenticated!: boolean;
    @Input() authItems!: AuthItem[]
    checkIcon: IconDefinition = faCheck
}
