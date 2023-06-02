import {GetGenreResponse} from "../../genres/models/GetGenreResponse";
import {PlatformType} from "./PlatformType";

export interface GetGameResponse {
    Key: string
    Name: string,
    Description: string,
    Genres: GetGenreResponse[],
    PlatformTypes: PlatformType[]
    Price: number
    UnitsInStock: number
    Discontinued: boolean
    PublisherCompanyName: string
}
