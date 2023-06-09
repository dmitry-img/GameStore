import {GetGenreResponse} from "../../genres/models/GetGenreResponse";
import {PlatformType} from "./PlatformType";
import {GetPublisherBriefResponse} from "../../publishers/models/GetPublisherBriefResponse";

export interface GetGameResponse {
    Key: string
    Name: string,
    Description: string,
    Genres: GetGenreResponse[],
    PlatformTypes: PlatformType[]
    Price: number
    UnitsInStock: number
    Discontinued: boolean
    Publisher: GetPublisherBriefResponse
    IsDeleted: boolean
}
