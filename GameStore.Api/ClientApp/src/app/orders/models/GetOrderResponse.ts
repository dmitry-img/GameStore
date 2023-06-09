import {OrderState} from "./OrderState";
import {GetOrderDetailResponse} from "./GetOrderDetailResponse";

export interface GetOrderResponse {
    CustomerId: number
    CustomerUsername: string
    OrderId: number
    OrderState: OrderState
    ShippedDate: Date
    TotalSum: number
    OrderDetails: GetOrderDetailResponse[]
}
