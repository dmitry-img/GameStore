export interface CreateGameRequest {
    Name: string
    Description: string
    GenreIds: number[]
    PlatformTypeIds: number[]
    Price: number
    UnitsInStock: number
    Discontinued: boolean
    PublisherId: number
}
