export interface CreateGameRequest {
  Name: string,
  Description: string,
  GenreIds: number[],
  PlatformTypeIds: number[]
}
