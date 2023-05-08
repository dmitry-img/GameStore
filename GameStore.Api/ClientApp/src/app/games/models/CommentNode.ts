import {GetCommentResponse} from "./GetCommentResponse";

export interface CommentNode {
    comment: GetCommentResponse;
    children?: CommentNode[];
}
