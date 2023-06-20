import {OrderState} from "./OrderState";
import {GetOrderDetailResponse} from "./GetOrderDetailResponse";

export interface GetOrderResponse {
    CustomerId: number
    CustomerUsername: string
    Id: number
    OrderState: OrderState
    ShippedDate: Date
    TotalSum: number
    OrderDetails: GetOrderDetailResponse[]
}
