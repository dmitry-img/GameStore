import {Injectable} from '@angular/core';
import {FormArray, FormControl} from "@angular/forms";
import {CheckboxListItem} from "../models/CheckBoxListItem";

@Injectable({
    providedIn: 'root'
})
export class CheckboxListService {
    onCheckBoxChange(
        event: any,
        id: number,
        items: CheckboxListItem[],
        control: FormArray
    ): void {
        const checked = event.target.checked;

        const updateCheckedState = (item: CheckboxListItem, checked: boolean) => {
            item.checked = checked;
            if (item.children) {
                item.children.forEach((child) => updateCheckedState(child, checked));
            }
        };

        const item = this.findItemById(id, items);
        if (item) {
            updateCheckedState(item, checked);

            if (item.parentId) {
                const parent = this.findItemById(item.parentId, items);
                if (parent) {
                    parent.checked = parent.children?.some((child) => child.checked) ?? false;
                }
            } else if (item.children) {
                item.children.forEach((child) => updateCheckedState(child, checked));
            }
        }

        const checkedIds = this.getCheckedItemIds(items);
        control.setValue(checkedIds);
        control.markAsTouched();
    }

    private findItemById(id: number, items: CheckboxListItem[]): CheckboxListItem | null {
        for (const item of items) {
            if (item.id === id) {
                return item;
            }
            if (item.children) {
                const foundItem = this.findItemById(id, item.children);
                if (foundItem) {
                    return foundItem;
                }
            }
        }
        return null;
    }

    private getCheckedItemIds(items: CheckboxListItem[]): number[] {
        let checkedIds: number[] = [];
        items.forEach((item) => {
            if (item.checked) {
                    checkedIds.push(item.id);
            }
            if (item.children) {
                checkedIds = [...checkedIds, ...this.getCheckedItemIds(item.children)];
            }
        });
        return checkedIds;
    }
}
