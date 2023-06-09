export interface UpdateGameRequest {
    Name: string,
    Description: string,
    GenreIds: number[],
    PlatformTypeIds: number[]
    PublisherId: number | null
    Price: number
    UnitsInStock: number
    Discontinued: boolean
}
