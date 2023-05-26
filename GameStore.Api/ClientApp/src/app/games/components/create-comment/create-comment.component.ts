import {Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output} from '@angular/core';
import {CommentService} from "../../services/comment.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {GetCommentResponse} from "../../models/GetCommentResponse";
import {Subscription} from "rxjs";
import {subscriptionLogsToBeFn} from "rxjs/internal/testing/TestScheduler";

@Component({
    selector: 'app-create-comment',
    templateUrl: './create-comment.component.html',
    styleUrls: ['./create-comment.component.scss']
})
export class CreateCommentComponent implements OnInit, OnDestroy {
    @Input() gameKey!: string
    parentComment!: GetCommentResponse | null;
    hasQuote: boolean = false
    replySubscription!: Subscription;
    quoteSubscription!: Subscription;
    createCommentForm!: FormGroup;

    constructor(
        private fb: FormBuilder,
        private commentService: CommentService
    ) { }

    ngOnInit(): void {
        this.createCommentForm = this.fb.group({
            Name: ['', Validators.required],
            Body: ['', Validators.required],
            HasQuote: [false],
            GameKey: [''],
            ParentCommentId: [''],
        });
        this.onCommentReplied();
        this.onCommentQuoted();
    }

    onSubmit(): void {
        if (this.createCommentForm.invalid) {
            return;
        }

        this.createCommentForm.get("GameKey")?.setValue(this.gameKey);
        this.createCommentForm.get("ParentCommentId")?.setValue(this.parentComment?.Id);
        this.createCommentForm.get("HasQuote")?.setValue(this.hasQuote)
        this.commentService.createComment(this.createCommentForm.value).subscribe(() => {
            this.commentService.emitNewComment(true);
        });

        this.createCommentForm.reset();
        this.parentComment = null;
        this.hasQuote = false;
    }

    onCommentReplied(): void {
        this.replySubscription = this.commentService.getEmittedReply$().subscribe((parentComment) =>{
            this.parentComment = parentComment;
            this.hasQuote = false;
        });
    }

    onCommentQuoted(): void {
        this.quoteSubscription = this.commentService.getEmittedQuote$().subscribe((parentComment) =>{
            this.parentComment = parentComment;
            this.hasQuote = true;
        });
    }

    ngOnDestroy(): void {
        this.replySubscription.unsubscribe();
        this.quoteSubscription.unsubscribe();
    }
}
