import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {GetGameResponse} from "../../../core/models/GetGameResponse";
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";
import {Genre} from "../../../core/models/Genre";
import {CommentNode} from "../../models/CommentNode";

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit{
  @Input() game!: GetGameResponse
  @Input() commentNodes!: CommentNode[]
  @Output() downloadGame = new EventEmitter<File>();
  parentGenres!: Genre[]

  ngOnInit(): void {
    this.parentGenres = this.game.Genres.filter(genre => genre.ParentGenreId == null);
  }
  getSubGenres(parent: Genre) : Genre[]{
    return this.game.Genres.filter(genre => genre.ParentGenreId == parent.Id);
  }

  onDownload() {
    this.downloadGame.emit();
  }
}
