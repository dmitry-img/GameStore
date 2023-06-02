// @ts-ignore

import {Pipe, PipeTransform} from '@angular/core';
import {GetGenreResponse} from "../../genres/models/GetGenreResponse";

@Pipe({
    name: 'subGenres'
})
export class SubGenresPipe implements PipeTransform {
    transform(genres: GetGenreResponse[], parent: GetGenreResponse): GetGenreResponse[] {
        return genres.filter(genre => genre.ParentGenreId === parent.Id);
    }
}
