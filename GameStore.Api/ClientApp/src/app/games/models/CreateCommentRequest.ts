export interface CreateCommentRequest {
    Name: string,
    Body: string,
    GameKey: string,
    ParentCommentId: number
}
