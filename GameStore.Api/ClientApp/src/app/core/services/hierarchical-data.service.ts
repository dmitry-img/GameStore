import {Injectable} from '@angular/core';
import {CheckboxListItem} from "../../shared/models/CheckBoxListItem";

@Injectable({
    providedIn: 'root'
})
export class HierarchicalDataService {
    convertToTreeStructure<T, R>(
        items: T[],
        idProp: keyof T,
        parentProp: keyof T | null,
        mappingFunction: (item: T) => R,
    ): R[] {
        const buildTree = (parent: T | null): R[] => {
            const children = items.filter((item) =>
                parent ? item[parentProp!] === parent[idProp] : !item[parentProp!]
            );

            const mappedChildren = children.map((child) => {
                const childChildren = buildTree(child);

                const mappedChild = mappingFunction(child);

                return childChildren.length
                    ? {...mappedChild, children: childChildren}
                    : mappedChild;
            });

            return mappedChildren;
        };

        return buildTree(null);
    }
}
