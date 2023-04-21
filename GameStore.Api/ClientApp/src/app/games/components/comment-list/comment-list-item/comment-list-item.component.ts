import {Component, Input} from '@angular/core';
import {GetCommentResponse} from "../../../../core/models/GetCommentResponse";

@Component({
  selector: 'app-comment-list-item',
  templateUrl: './comment-list-item.component.html',
  styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent {
  @Input() comments!: GetCommentResponse[]
}
