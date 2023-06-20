import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {UserListComponent} from "./components/user-list/user-list.component";
import {RoleListComponent} from "./components/role-list/role-list.component";
import {MainAdminPanelPageComponent} from "./pages/main-admin-panel-page/main-admin-panel-page.component";
import {UserListPageComponent} from "./pages/user-list-page/user-list-page.component";
import {RoleListPageComponent} from "./pages/role-list-page/role-list-page.component";

const routes: Routes = [
    {
        path: '',
        component: MainAdminPanelPageComponent,
        children: [
            { path: '', redirectTo: 'users/list/1', pathMatch: 'full' },
            { path: 'user/list/:page', component: UserListPageComponent },
            { path: 'role/list/:page', component: RoleListPageComponent },
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPanelRoutingModule { }
