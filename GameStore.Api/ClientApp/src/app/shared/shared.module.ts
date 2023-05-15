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
        InfoModalComponent
    ],
    imports: [
        CommonModule,
        RouterLink,
        ReactiveFormsModule,
        ModalModule.forRoot()
    ]
})
export class SharedModule {
}
