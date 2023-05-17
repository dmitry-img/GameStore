export interface PaginationResult<T> {
    Items: T[]
    TotalItems: number
    PageSize: number
    CurrentPage: number
}
