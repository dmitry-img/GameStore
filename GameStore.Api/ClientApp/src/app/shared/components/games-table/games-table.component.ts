import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {GamesTableItem} from "../../models/GamesTableItem";

@Component({
  selector: 'app-games-table',
  templateUrl: './games-table.component.html',
  styleUrls: ['./games-table.component.scss']
})
export class GamesTableComponent implements OnInit{
    @Input() items!: GamesTableItem[];
    @Input() showDelete: boolean = false;
    @Output() onDelete: EventEmitter<any> = new EventEmitter();
    totalPrice!: number;

    ngOnInit(): void {
        this.totalPrice = this.getTotalPrice();
    }
    getTotalPrice() {
        return this.items.reduce((total, item) => total + item.Quantity * item.GamePrice, 0);
    }

    deleteItem(itemKey: any) {
        this.onDelete.emit(itemKey);
    }
}
