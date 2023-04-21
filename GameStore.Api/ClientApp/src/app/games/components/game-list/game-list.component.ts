import {Component, Input, OnInit} from '@angular/core';
import {GetGameResponse} from "../../../core/models/GetGameResponse";

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent{
  @Input() games!: GetGameResponse[];
  constructor() {
    console.log(this.games)
  }
}
