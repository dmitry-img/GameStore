import {Component, Input, OnInit} from '@angular/core';
import {CommentNode} from "../../../models/CommentNode";

@Component({
  selector: 'app-comment-list-item',
  templateUrl: './comment-list-item.component.html',
  styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent{
  @Input() commentNode!: CommentNode;
  @Input() parentNode: CommentNode | null = null;
  @Input() gameKey!: string;
  clickedReply: boolean = false;

  navigateToParent(id: number): void {
    const element = document.getElementById(`comment-${id}`);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  }
}
