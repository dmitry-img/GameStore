import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CreatePublisherPageComponent} from './pages/create-publisher-page/create-publisher-page.component';
import {PublisherDetailsPageComponent} from './pages/publisher-details-page/publisher-details-page.component';
import {PublisherDetailsComponent} from './components/publisher-details/publisher-details.component';
import {CreatePublisherComponent} from './components/create-publisher/create-publisher.component';
import {ReactiveFormsModule} from "@angular/forms";
import {SharedModule} from "../shared/shared.module";


@NgModule({
    declarations: [
        CreatePublisherPageComponent,
        PublisherDetailsPageComponent,
        PublisherDetailsComponent,
        CreatePublisherComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        SharedModule
    ]
})
export class PublishersModule {
}
