export interface CreateCommentRequest {
    Name: string
    Body: string
    HasQuote: string
    GameKey: string
    ParentCommentId: number
}
