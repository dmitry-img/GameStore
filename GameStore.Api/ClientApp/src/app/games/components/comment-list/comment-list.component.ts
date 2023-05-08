import {Component, Input, OnInit} from '@angular/core';
import {GetCommentResponse} from "../../models/GetCommentResponse";
import {CommentNode} from "../../models/CommentNode";

@Component({
    selector: 'app-comment-list',
    templateUrl: './comment-list.component.html',
    styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent {
    @Input() commentNodes!: CommentNode[]
    @Input() gameKey!: string;
    @Input() parentNode: CommentNode | null = null;
}
