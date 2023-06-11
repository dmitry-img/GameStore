import {Component, Input, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {GetCommentResponse} from "../../models/GetCommentResponse";
import {CommentNode} from "../../models/CommentNode";
import {CommentService} from "../../services/comment.service";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";

@Component({
    selector: 'app-comment-list',
    templateUrl: './comment-list.component.html',
    styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent {
    @Input() commentNodes!: CommentNode[]
    @Input() gameKey!: string;
    @Input() parentNode: CommentNode | null = null;
    @Input() userRole!: string | null;
    @Input() isUserBanned!: boolean
    @Input() isGameDeleted!: boolean;
    @Input() rolesAllowedToComment!: string[];
    @ViewChild('commentDeleteModalBody') commentDeleteModalBody!: TemplateRef<any>;

    bsModalRef!: BsModalRef;

    constructor(private commentService: CommentService, private modalService: BsModalService) { }

    onDelete(id: number): void {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.commentDeleteModalBody
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.commentService.deleteComment(id).subscribe(() =>{
                this.commentService.emitDeletedComment(true);
                this.bsModalRef.hide();
            });
        });
    }

    onReply(parentComment: GetCommentResponse): void {
        this.commentService.emitReply(parentComment)
    }

    onQuote(parentComment: GetCommentResponse): void {
        this.commentService.emitQuote(parentComment)
    }
}
