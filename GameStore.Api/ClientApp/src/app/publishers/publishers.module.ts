import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CreatePublisherPageComponent} from './pages/create-publisher-page/create-publisher-page.component';
import {PublisherDetailsPageComponent} from './pages/publisher-details-page/publisher-details-page.component';
import {PublisherDetailsComponent} from './components/publisher-details/publisher-details.component';
import {ReactiveFormsModule} from "@angular/forms";
import {SharedModule} from "../shared/shared.module";
import { PublisherListPageComponent } from './pages/publisher-list-page/publisher-list-page.component';
import { PublisherListComponent } from './components/publisher-list/publisher-list.component';
import {RouterLink} from "@angular/router";
import { UpdatePublisherPageComponent } from './pages/update-publisher-page/update-publisher-page.component';
import {NgxSelectModule} from "ngx-select-ex";


@NgModule({
    declarations: [
        CreatePublisherPageComponent,
        PublisherDetailsPageComponent,
        PublisherDetailsComponent,
        PublisherListPageComponent,
        PublisherListComponent,
        UpdatePublisherPageComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        SharedModule,
        RouterLink,
        NgxSelectModule
    ]
})
export class PublishersModule {
}
