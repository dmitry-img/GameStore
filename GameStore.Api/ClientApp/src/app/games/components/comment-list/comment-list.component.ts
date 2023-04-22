import {Component, Input} from '@angular/core';
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent {
  @Input() comments!: GetCommentResponse[]
}
