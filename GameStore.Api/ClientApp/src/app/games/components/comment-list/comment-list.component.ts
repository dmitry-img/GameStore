import {Component, Input, OnInit} from '@angular/core';
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";
import {CommentNode} from "../../models/CommentNode";

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent implements OnInit{
  @Input() commentNodes!: CommentNode[]



  replyClicked(comments: any) {

  }

  ngOnInit(): void {

  }
}
