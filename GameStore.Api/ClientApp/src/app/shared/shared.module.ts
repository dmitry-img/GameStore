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
        DropdownComponent
    ],
    imports: [
        CommonModule,
        RouterLink,
        ReactiveFormsModule
    ]
})
export class SharedModule {
}
