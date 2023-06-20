import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CommentNode} from "../../../models/CommentNode";
import {GetCommentResponse} from "../../../models/GetCommentResponse";

@Component({
    selector: 'app-comment-list-item',
    templateUrl: './comment-list-item.component.html',
    styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent {
    @Input() commentNode!: CommentNode;
    @Input() parentNode: CommentNode | null = null;
    @Input() gameKey!: string;
    @Input() isGameDeleted!: boolean;
    @Input() isUserBanned!: boolean
    @Input() rolesAllowedToComment!: string[]
    @Output() delete: EventEmitter<number> = new EventEmitter<number>();
    @Output() reply: EventEmitter<GetCommentResponse> = new EventEmitter<GetCommentResponse>();
    @Output() quote: EventEmitter<GetCommentResponse> = new EventEmitter<GetCommentResponse>();

    navigateToParent(id: number): void {
        const element = document.getElementById(`comment-${id}`);
        if (element) {
            element.scrollIntoView({behavior: 'smooth'});
        }
    }

    onReply(parentComment: GetCommentResponse): void {
       this.reply.emit(parentComment);
    }

    onQuote(parentComment: GetCommentResponse): void {
        this.quote.emit(parentComment);
    }

    onDelete(id: number): void {
        this.delete.emit(id);
    }
}
