import {Component, Input} from '@angular/core';
import {NavItem} from "../../../models/NavItem";

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.scss']
})
export class TopNavComponent {
    @Input() items!: NavItem[];
    @Input() contacts!: NavItem[];
}
