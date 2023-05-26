import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {GamesTableItem} from "../../models/GamesTableItem";

@Component({
  selector: 'app-games-table',
  templateUrl: './games-table.component.html',
  styleUrls: ['./games-table.component.scss']
})
export class GamesTableComponent implements OnChanges{
    @Input() items!: GamesTableItem[];
    @Input() showDelete: boolean = false;
    @Output() onDelete: EventEmitter<any> = new EventEmitter();
    totalPrice!: number;

    ngOnChanges(changes: SimpleChanges): void {
        this.totalPrice = this.getTotalPrice();
    }

    getTotalPrice(): number {
        return this.items.reduce((total, item) => total + item.Quantity * item.GamePrice, 0);
    }

    deleteItem(itemKey: any): void {
        this.onDelete.emit(itemKey);
    }
}
