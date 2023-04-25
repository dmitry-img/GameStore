import {Component, OnInit} from '@angular/core';
import {GameService} from "../../../core/service/game.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements  OnInit{
  gamesCount!: number;
  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.gameService.getCount().subscribe(count => {
      this.gamesCount = count;
    })
  }
}
