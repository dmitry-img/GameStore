import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {CommentService} from "../../services/comment.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {GetCommentResponse} from "../../../core/models/GetCommentResponse";

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.scss']
})
export class CreateCommentComponent implements OnInit{
  @Input() parentComment: GetCommentResponse | null  = null
  createCommentForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private commentService: CommentService,
    private route: ActivatedRoute ) { }

  ngOnInit(): void {
    this.createCommentForm = this.fb.group({
      Name: ['', Validators.required],
      Body: ['', Validators.required],
      GameKey: [''],
      ParentCommentId: [''],
    });
  }

  onSubmit(): void {
    this.route.params.subscribe(data =>{
      this.createCommentForm.get("GameKey")?.setValue(data['key']);
      this.createCommentForm.get("ParentCommentId")?.setValue(this.parentComment?.Id);
      this.commentService.emitComment(this.createCommentForm.value);
    })
  }
}
