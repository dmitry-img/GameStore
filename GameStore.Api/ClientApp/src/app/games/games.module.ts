import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentListComponent } from './components/comment-list/comment-list.component';
import { GameListComponent } from './components/game-list/game-list.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { CreateGameComponent } from './components/create-game/create-game.component';
import { GameListPageComponent } from './pages/game-list-page/game-list-page.component';
import { GameDetailsPageComponent } from './pages/game-details-page/game-details-page.component';
import { CreateGamePageComponent } from './pages/create-game-page/create-game-page.component';
import {RouterLink} from "@angular/router";
import { CommentListItemComponent } from './components/comment-list/comment-list-item/comment-list-item.component';



@NgModule({
  declarations: [
    CommentListComponent,
    GameListComponent,
    GameDetailsComponent,
    CreateGameComponent,
    GameListPageComponent,
    GameDetailsPageComponent,
    CreateGamePageComponent,
    CommentListItemComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
  ],
  exports:[
    GameListPageComponent,
    GameDetailsPageComponent,
    CreateGamePageComponent
  ]
})
export class GamesModule { }