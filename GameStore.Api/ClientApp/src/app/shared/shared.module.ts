import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HeaderComponent} from './components/header/header.component';
import {FooterComponent} from './components/footer/footer.component';
import {RouterLink} from "@angular/router";
import {ValidationMessageComponent} from './components/validation-message/validation-message.component';
import {InputFieldComponent} from './components/input-field/input-field.component';
import {ReactiveFormsModule} from "@angular/forms";
import {FormControlPipe} from './pipes/form-control.pipe';
import {TextAreaFieldComponent} from './components/text-area-field/text-area-field.component';
import {CheckboxListComponent} from './components/checkbox-list/checkbox-list.component';
import {FormArrayPipe} from './pipes/form-array.pipe';
import {DropdownComponent} from './components/dropdown/dropdown.component';
import {GamesTableComponent} from './components/games-table/games-table.component';
import {ModalModule} from "ngx-bootstrap/modal";
import {InfoModalComponent} from './components/info-modal/info-modal.component';
import { CollapseComponent } from './components/collapse/collapse.component';
import {CollapseModule} from "ngx-bootstrap/collapse";
import { PaginationNavComponent } from './components/pagination-nav/pagination-nav.component';
import { ConfirmationModalComponent } from './components/confirmation-modal/confirmation-modal.component';
import { ErrorPageComponent } from './pages/error-page/error-page.component';
import {HasRoleDirective} from "./directives/has-role.directive";
import {ExceptRoleDirective} from "./directives/except-role.directive";
import { TopNavComponent } from './components/header/top-nav/top-nav.component';
import { MainNavComponent } from './components/header/main-nav/main-nav.component';
import { SubNavComponent } from './components/header/sub-nav/sub-nav.component';
import { LogoComponent } from './components/header/main-nav/logo/logo.component';
import { AuthItemsComponent } from './components/header/main-nav/auth-items/auth-items.component';
import { MainItemsComponent } from './components/header/main-nav/main-items/main-items.component';
import { MainFooterComponent } from './components/footer/main-footer/main-footer.component';
import { GetInTouchComponent } from './components/footer/get-in-touch/get-in-touch.component';
import { TwitterUpdatesComponent } from './components/footer/twitter-updates/twitter-updates.component';
import { FromTheBlogComponent } from './components/footer/from-the-blog/from-the-blog.component';
import { InfoBlockComponent } from './components/footer/info-block/info-block.component';
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import { AdvertisingSidebarComponent } from './components/advertising-sidebar/advertising-sidebar.component';

@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        ValidationMessageComponent,
        InputFieldComponent,
        FormControlPipe,
        TextAreaFieldComponent,
        CheckboxListComponent,
        FormArrayPipe,
        DropdownComponent,
        GamesTableComponent,
        InfoModalComponent,
        CollapseComponent,
        PaginationNavComponent,
        ConfirmationModalComponent,
        ErrorPageComponent,
        HasRoleDirective,
        ExceptRoleDirective,
        TopNavComponent,
        MainNavComponent,
        SubNavComponent,
        LogoComponent,
        AuthItemsComponent,
        MainItemsComponent,
        MainFooterComponent,
        GetInTouchComponent,
        TwitterUpdatesComponent,
        FromTheBlogComponent,
        InfoBlockComponent,
        AdvertisingSidebarComponent,
    ],
    exports: [
        HeaderComponent,
        FooterComponent,
        InputFieldComponent,
        FormControlPipe,
        ValidationMessageComponent,
        TextAreaFieldComponent,
        CheckboxListComponent,
        FormArrayPipe,
        DropdownComponent,
        GamesTableComponent,
        InfoModalComponent,
        CollapseComponent,
        PaginationNavComponent,
        HasRoleDirective,
        ExceptRoleDirective,
        AdvertisingSidebarComponent
    ],
    imports: [
        CommonModule,
        RouterLink,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CollapseModule.forRoot(),
        FontAwesomeModule
    ]
})
export class SharedModule {
}
