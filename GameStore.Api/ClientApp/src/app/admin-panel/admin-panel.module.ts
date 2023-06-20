import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';
import {MainAdminPanelPageComponent} from "./pages/main-admin-panel-page/main-admin-panel-page.component";
import {UserListComponent} from "./components/user-list/user-list.component";
import {RoleListComponent} from "./components/role-list/role-list.component";
import {RoleListPageComponent} from "./pages/role-list-page/role-list-page.component";
import {UserListPageComponent} from "./pages/user-list-page/user-list-page.component";
import {SharedModule} from "../shared/shared.module";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
    declarations: [
        MainAdminPanelPageComponent,
        UserListComponent,
        RoleListComponent,
        UserListComponent,
        RoleListPageComponent,
        UserListPageComponent,
    ],
    imports: [
        CommonModule,
        AdminPanelRoutingModule,
        SharedModule,
        ReactiveFormsModule,
    ]
})
export class AdminPanelModule { }
