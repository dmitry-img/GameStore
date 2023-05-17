import {Component, Input} from '@angular/core';
import {DropDownItem} from "../../models/DropDownItem";
import {FormControl} from "@angular/forms";

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent {
    @Input() items!: DropDownItem[];
    @Input() labelText!: string;
    @Input() control!: FormControl;
    @Input() placeholder: string = 'Select an option'
    @Input() showPlaceholder: boolean = true;
}
