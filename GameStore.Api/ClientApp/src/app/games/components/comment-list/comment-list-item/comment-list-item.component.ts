import {Component, Input, OnInit} from '@angular/core';
import {GetCommentResponse} from "../../../../core/models/GetCommentResponse";
import {CommentNode} from "../../../models/CommentNode";
import {ActivatedRoute, NavigationExtras, Route, Router} from "@angular/router";

@Component({
  selector: 'app-comment-list-item',
  templateUrl: './comment-list-item.component.html',
  styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent{
  @Input() commentNode!: CommentNode;
  @Input() parentNode: CommentNode | null = null;
  clickedReply: boolean = false;

  constructor(private router: Router) {
  }

  navigateToParent(id: number) {
    this.router.navigate([], { fragment: `comment-${id}` }).then(() => {
      const element = document.getElementById(`comment-${id}`);
      if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
      }
    });
  }
}
