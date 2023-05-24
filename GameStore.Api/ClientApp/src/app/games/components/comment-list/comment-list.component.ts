import {Component, Input, OnInit} from '@angular/core';
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
    bsModalRef!: BsModalRef;

    constructor(private commentService: CommentService, private modalService: BsModalService) { }

    onDelete(id: number): void {
        const initialState = {
            title: 'Are you sure?',
            message: 'The comment will be deleted!',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel'
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.commentService.deleteComment(id).subscribe(() =>{
                this.commentService.emitDeletedComment(true);
            });
        });
    }
}
