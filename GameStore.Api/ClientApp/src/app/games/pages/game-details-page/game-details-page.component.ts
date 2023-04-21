import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {GetGameResponse} from "../../../core/models/GetGameResponse";
import {GameService} from "../../services/game.service";
import {CommentService} from "../../services/comment.service";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";

@Component({
  selector: 'app-game-details-page',
  templateUrl: './game-details-page.component.html',
  styleUrls: ['./game-details-page.component.scss']
})
export class GameDetailsPageComponent implements OnInit{
  game!: GetGameResponse
  comments!: GetCommentResponse[]
  constructor( private route: ActivatedRoute,
    private gameService: GameService,
    private commentService: CommentService) { }

  ngOnInit(): void {
    this.route.params.subscribe(data => {
      this.gameService.getGameByKey(data['key']).subscribe({
        next: (games: GetGameResponse) => {
          this.game = games;
        },
        error: (error) => {
          console.log(error);
        }
      })

      this.commentService.getCommentList(data['key']).subscribe({
        next: (comments: GetCommentResponse[]) => {
          this.comments = comments;
        },
        error: (error) => {
          console.log(error);
        }
      })
    });
  }
}
