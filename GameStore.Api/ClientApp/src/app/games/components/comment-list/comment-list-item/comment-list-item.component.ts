import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CommentNode} from "../../../models/CommentNode";

@Component({
    selector: 'app-comment-list-item',
    templateUrl: './comment-list-item.component.html',
    styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent {
    @Input() commentNode!: CommentNode;
    @Input() parentNode: CommentNode | null = null;
    @Input() gameKey!: string;
    @Output() delete: EventEmitter<number> = new EventEmitter<number>();
    clickedReply: boolean = false;
    clickedQuote: boolean = false;

    navigateToParent(id: number): void {
        const element = document.getElementById(`comment-${id}`);
        if (element) {
            element.scrollIntoView({behavior: 'smooth'});
        }
    }

    onReply(): void {
        if(this.clickedReply){
            this.clickedReply = !this.clickedReply
            return;
        }
        this.clickedReply = true;
        this.clickedQuote = false;
    }

    onQuote(): void {
        if(this.clickedQuote){
            this.clickedQuote = !this.clickedQuote
            return;
        }
        this.clickedReply = false;
        this.clickedQuote = true;
    }

    onDelete(id: number): void {
        this.delete.emit(id);
    }
}
