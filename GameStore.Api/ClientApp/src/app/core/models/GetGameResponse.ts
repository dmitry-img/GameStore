import {Genre} from "./Genre";
import {PlatformType} from "./PlatformType";

export interface GetGameResponse {
  Key: string
  Name: string,
  Description: string,
  Genres: Genre[],
  PlatformTypes: PlatformType[]
}
