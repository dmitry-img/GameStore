export interface CheckboxListItem {
    id: number;
    label: string;
    checked: boolean;
    children?: CheckboxListItem[];
    parentId?: number
}
