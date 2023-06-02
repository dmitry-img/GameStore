import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenreListPageComponent } from './pages/genre-list-page/genre-list-page.component';
import { GenreListComponent } from './components/genre-list/genre-list.component';
import {SharedModule} from "../shared/shared.module";



@NgModule({
  declarations: [
    GenreListPageComponent,
    GenreListComponent
  ],
    imports: [
        CommonModule,
        SharedModule
    ]
})
export class GenresModule { }
