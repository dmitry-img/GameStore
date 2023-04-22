import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {GameListPageComponent} from "./games/pages/game-list-page/game-list-page.component";
import {GameDetailsPageComponent} from "./games/pages/game-details-page/game-details-page.component";
import {CreateGameComponent} from "./games/components/create-game/create-game.component";
import {CreateGamePageComponent} from "./games/pages/create-game-page/create-game-page.component";

const routes: Routes = [
  { path: '', redirectTo: '/game/list', pathMatch: 'full' },
  { path: 'game/list', component: GameListPageComponent },
  { path: 'game/create', component: CreateGamePageComponent },
  { path: 'game/:key', component: GameDetailsPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
