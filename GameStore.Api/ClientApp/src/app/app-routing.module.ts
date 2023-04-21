import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {GameListPageComponent} from "./games/pages/game-list-page/game-list-page.component";
import {GameDetailsPageComponent} from "./games/pages/game-details-page/game-details-page.component";

const routes: Routes = [
  { path: '', redirectTo: '/game/list', pathMatch: 'full' },
  { path: 'game/list', component: GameListPageComponent },
  { path: 'game/:key', component: GameDetailsPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
