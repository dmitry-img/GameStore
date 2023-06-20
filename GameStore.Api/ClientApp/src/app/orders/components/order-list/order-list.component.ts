import {Component, EventEmitter, Input, OnChanges, Output} from '@angular/core';
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetRoleResponse} from "../../../admin-panel/models/GetRoleResponse";
import {GetOrderResponse} from "../../models/GetOrderResponse";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import { OrderState } from '../../models/OrderState';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnChanges{
    @Input() paginatedOrders!: PaginationResult<GetOrderResponse>;
    @Input() orderStates!: DropDownItem[];
    @Output() update = new EventEmitter<GetOrderResponse>();
    orderStateValues!: {order: GetOrderResponse, stateValue: string}[];
    OrderStateEnum = OrderState

    ngOnChanges(): void {
        this.orderStateValues = this.paginatedOrders.Items.map(order => ({
            order: order,
            stateValue: this.getOrderStateValue(order.OrderState)
        }));
    }

    onUpdate(order: GetOrderResponse): void {
        this.update.emit(order);
    }

    private getOrderStateValue(orderStateId: number) {
        const state = this.orderStates.find((item: DropDownItem) => item.Id === orderStateId);
        return state ? state.Value : '';
    }
}
