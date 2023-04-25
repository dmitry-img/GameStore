import {GetCommentResponse} from "../../core/models/GetCommentResponse";

export interface CommentNode {
  comment: GetCommentResponse;
  children?: CommentNode[];
}
