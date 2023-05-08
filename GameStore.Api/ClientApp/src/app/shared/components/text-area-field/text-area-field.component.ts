import {Component, Input} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
    selector: 'app-text-area-field',
    templateUrl: './text-area-field.component.html',
    styleUrls: ['./text-area-field.component.scss']
})
export class TextAreaFieldComponent {
    @Input() control!: FormControl;
    @Input() isRequired!: boolean;
    @Input() labelText!: string
}
