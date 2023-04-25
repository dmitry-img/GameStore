import {Component, Input, OnInit} from '@angular/core';
import {GetCommentResponse} from "../../../../core/models/GetCommentResponse";
import {CommentNode} from "../../../models/CommentNode";
import {ActivatedRoute, Route, Router} from "@angular/router";

@Component({
  selector: 'app-comment-list-item',
  templateUrl: './comment-list-item.component.html',
  styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent{
  @Input() commentNode!: CommentNode;
  @Input() parentNode: CommentNode | null = null;
  clickedReply: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute) {
  }

  navigateToParent(id: number) {
    this.router.navigate([], { relativeTo: this.route, fragment: `comment-${id}`})
  }
}
