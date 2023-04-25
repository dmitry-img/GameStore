import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {GetGameResponse} from "../../../core/models/GetGameResponse";
import {GameService} from "../../../core/service/game.service";
import {CommentService} from "../../services/comment.service";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";
import {CreateCommentRequest} from "../../../core/models/CreateCommentRequest";
import {ToastrService} from "ngx-toastr";
import {Subscription} from "rxjs";
import {CommentNode} from "../../models/CommentNode";

@Component({
  selector: 'app-game-details-page',
  templateUrl: './game-details-page.component.html',
  styleUrls: ['./game-details-page.component.scss']
})
export class GameDetailsPageComponent implements OnInit, OnDestroy{
  game!: GetGameResponse
  commentNodes!: CommentNode[]
  newCommentSubscription!: Subscription;
  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private commentService: CommentService,
    private toaster: ToastrService) { }

  ngOnInit(): void {
    this.route.params.subscribe(data => {
      this.updateGame(data['key'])
      this.updateCommentListArray(data['key'])
    });
    this.onCommentCreated()
  }

  private buildCommentTree(comments: GetCommentResponse[]): CommentNode[] {
    const commentMap = new Map<number, CommentNode>();
    const roots: CommentNode[] = [];

    comments.forEach(comment => {
      const node: CommentNode = { comment };
      commentMap.set(comment.Id, node);
      const parent = commentMap.get(comment.ParentCommentId);
      if (parent) {
        if (!parent.children) {
          parent.children = [];
        }
        parent.children.unshift(node);
      } else {
        roots.unshift(node);
      }
    });
    return roots;
  }

  private updateCommentListArray(gameKey: string){
    this.commentService.getCommentList(gameKey).subscribe({
      next: (comments: GetCommentResponse[]) => {
        this.commentNodes = this.buildCommentTree(comments);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  private updateGame(key: string){
    this.gameService.getGameByKey(key).subscribe({
      next: (games: GetGameResponse) => {
        this.game = games;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  onDownloadGame(){
    this.route.params.subscribe(data => {
        this.gameService.downloadGame(data['key'], this.game.Name);
    });
  }

  onCommentCreated(){
    this.newCommentSubscription = this.commentService.getEmittedComment().subscribe((newComment: CreateCommentRequest) => {
      this.commentService.createComment(newComment).subscribe({
        next: () => {
          this.updateCommentListArray(newComment.GameKey)
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
