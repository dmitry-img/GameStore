import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {GetGameResponse} from "../../../core/models/GetGameResponse";
import {GameService} from "../../services/game.service";
import {CommentService} from "../../services/comment.service";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";
import {CreateCommentRequest} from "../../../core/models/CreateCommentRequest";
import {ToastrService} from "ngx-toastr";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-game-details-page',
  templateUrl: './game-details-page.component.html',
  styleUrls: ['./game-details-page.component.scss']
})
export class GameDetailsPageComponent implements OnInit, OnDestroy{
  game!: GetGameResponse
  comments!: GetCommentResponse[]
  newCommentSubscription!: Subscription;
  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private commentService: CommentService,
    private toaster: ToastrService) { }

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

    this.onCommentCreated()

    this.commentService.getEmittedComment().subscribe((comment: CreateCommentRequest) =>{
      this.route.params.subscribe(data => {
        this.commentService.getCommentList(data['key']).subscribe({
          next: (comments: GetCommentResponse[]) => {
            this.comments = comments;
            console.log(this.comments)
          },
          error: (error) => {
            console.log(error);
          }
        })
      });
    });
  }

  onDownloadGame(){
    this.route.params.subscribe(data => {
        this.gameService.downloadGame(data['key'], this.game.Name);
    });
  }

  onCommentCreated(){
    this.newCommentSubscription = this.commentService.getEmittedComment().subscribe((newComment: CreateCommentRequest) => {
      console.log("+")
      this.commentService.createComment(newComment).subscribe({
        next: () => {
          this.toaster.success("The comment has been added successfully!")
        },
        error: (error) =>{
          const errorArray = error.error.Message.split(',');
          errorArray.forEach((message: string) => {
            this.toaster.error(message);
          });
        }
      });
    })
  }

  ngOnDestroy(): void {
    this.newCommentSubscription.unsubscribe();
  }
}
