import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CommentListComponent} from './components/comment-list/comment-list.component';
import {GameListComponent} from './components/game-list/game-list.component';
import {GameDetailsComponent} from './components/game-details/game-details.component';
import {CreateGameComponent} from './components/create-game/create-game.component';
import {GameListPageComponent} from './pages/game-list-page/game-list-page.component';
import {GameDetailsPageComponent} from './pages/game-details-page/game-details-page.component';
import {CreateGamePageComponent} from './pages/create-game-page/create-game-page.component';
import {RouterLink} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CreateCommentComponent} from './components/create-comment/create-comment.component';
import {CommentListItemComponent} from './components/comment-list/comment-list-item/comment-list-item.component';
import {SharedModule} from "../shared/shared.module";
import {SubGenresPipe} from './pipes/sub-genres.pipe';
import { GameListSidebarComponent } from './components/game-list-sidebar/game-list-sidebar.component';
import { BanPageComponent } from './pages/ban-page/ban-page.component';
import { BanComponent } from './components/ban/ban.component';
import { GameListManagementPageComponent } from './pages/game-list-management-page/game-list-management-page.component';
import { GameListManagementComponent } from './components/game-list-management/game-list-management.component';
import { UpdateGamePageComponent } from './pages/update-game-page/update-game-page.component';
import { UpdateGameComponent } from './components/update-game/update-game.component';
import {AuthModule} from "../auth/auth.module";


@NgModule({
    declarations: [
        CommentListComponent,
        GameListComponent,
        GameDetailsComponent,
        CreateGameComponent,
        GameListPageComponent,
        GameDetailsPageComponent,
        CreateGamePageComponent,
        CreateCommentComponent,
        CommentListItemComponent,
        SubGenresPipe,
        GameListSidebarComponent,
        BanPageComponent,
        BanComponent,
        GameListManagementPageComponent,
        GameListManagementComponent,
        UpdateGamePageComponent,
        UpdateGameComponent
    ],
    imports: [
        CommonModule,
        RouterLink,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        FormsModule,
        SharedModule,
        AuthModule,
    ],
    exports: [
        GameListPageComponent,
        GameDetailsPageComponent,
        CreateGamePageComponent
    ]
})
export class GamesModule {
}
