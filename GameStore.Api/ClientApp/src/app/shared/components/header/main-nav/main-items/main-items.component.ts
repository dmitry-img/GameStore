import {Component, Input} from '@angular/core';
import {MainNavItem} from "../../../../models/MainNavItem";

@Component({
  selector: 'app-main-items',
  templateUrl: './main-items.component.html',
  styleUrls: ['./main-items.component.scss']
})
export class MainItemsComponent {
    @Input() mainItems!: MainNavItem[];
    @Input() gamesCount!: number;
}
