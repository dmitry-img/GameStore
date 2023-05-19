export interface GetCommentResponse {
    Id: number,
    Name: string,
    Body: string,
    HasQuote: boolean
    GameKey: string,
    ParentCommentId: number | null
}
