import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {CommentService} from "../../services/comment.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {GetCommentResponse} from "../../models/GetCommentResponse";

@Component({
    selector: 'app-create-comment',
    templateUrl: './create-comment.component.html',
    styleUrls: ['./create-comment.component.scss']
})
export class CreateCommentComponent implements OnInit {
    @Input() parentComment: GetCommentResponse | null = null
    @Input() gameKey!: string
    createCommentForm!: FormGroup;

    constructor(
        private fb: FormBuilder,
        private commentService: CommentService
    ) { }

    ngOnInit(): void {
        this.createCommentForm = this.fb.group({
            Name: ['', Validators.required],
            Body: ['', Validators.required],
            GameKey: [''],
            ParentCommentId: [''],
        });
    }

    onSubmit(): void {
        if (this.createCommentForm.invalid) {
            return;
        }

        this.createCommentForm.get("GameKey")?.setValue(this.gameKey);
        this.createCommentForm.get("ParentCommentId")?.setValue(this.parentComment?.Id);
        this.commentService.createComment(this.createCommentForm.value).subscribe(() => {
            this.commentService.emitComment(true);
        });
        this.createCommentForm.reset();
    }
}
