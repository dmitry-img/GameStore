export interface UpdateGameRequest {
    Name: string,
    Description: string,
    GenreIds: number[],
    PlatformTypeIds: number[]
    Price: number
    UnitsInStock: number
    Discontinued: boolean
}
