import {Component, HostBinding, Input, OnInit} from '@angular/core';
import {CheckboxListItem} from "../../models/CheckBoxListItem";
import {FormArray, FormControl} from "@angular/forms";
import {CheckboxListService} from "../../services/checkbox-list.service";

@Component({
    selector: 'app-checkbox-list',
    templateUrl: './checkbox-list.component.html',
    styleUrls: ['./checkbox-list.component.scss']
})
export class CheckboxListComponent {
    @Input() items!: CheckboxListItem[];
    @Input() parentItems?: CheckboxListItem[];
    @Input() control!: FormArray;
    @Input() labelText?: string;
    @Input() isRequired?: boolean;
    uniqueInstanceId!: string;

    constructor(private checkboxListService: CheckboxListService) {
        this.uniqueInstanceId = 'checkbox-list-' + Math.random().toString(36).substring(2, 11);
    }

    onCheckboxChange(event: Event, id: number): void {
        const items = this.parentItems ?? this.items;
        this.checkboxListService.onCheckBoxChange(event, id, items, this.control);
    }
}
