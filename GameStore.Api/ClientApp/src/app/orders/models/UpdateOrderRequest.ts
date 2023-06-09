import { OrderState } from "./OrderState"
import {UpdateOrderDetailRequest} from "./UpdateOrderDetailRequest";

export interface UpdateOrderRequest{
    OrderState: OrderState
    OrderDetails: UpdateOrderDetailRequest[]
}
