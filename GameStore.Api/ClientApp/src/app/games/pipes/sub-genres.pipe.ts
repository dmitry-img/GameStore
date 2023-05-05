// @ts-ignore

import { Pipe, PipeTransform } from '@angular/core';
import {Genre} from "../models/Genre";

@Pipe({
  name: 'subGenres'
})
export class SubGenresPipe implements PipeTransform {
  transform(genres: Genre[], parent: Genre): Genre[] {
    return genres.filter(genre => genre.ParentGenreId === parent.Id);
  }
}
