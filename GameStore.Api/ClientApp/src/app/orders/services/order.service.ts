import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpParams} from "@angular/common/http";
import {GetOrderResponse} from "../models/GetOrderResponse";
import {PaginationResult} from "../../shared/models/PaginationResult";
import {PaginationRequest} from "../../shared/models/PaginationRequest";
import {OrderState} from "../models/OrderState";
import {UpdateOrderRequest} from "../models/UpdateOrderRequest";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
    private baseUrl = '/api/orders';
    constructor(private http: HttpClient) { }

    createOrder() : Observable<GetOrderResponse>{
        return this.http.post<GetOrderResponse>(`${this.baseUrl}/create`,null);
    }

    getAllOrdersWithPagination(paginationRequest: PaginationRequest): Observable<PaginationResult<GetOrderResponse>>{
        let params = new HttpParams()
            .set('PageNumber', paginationRequest.PageNumber.toString())
            .set('PageSize', paginationRequest.PageSize.toString());

        return this.http.get<PaginationResult<GetOrderResponse>>(`${this.baseUrl}/paginated-list`, { params });
    }

    updateOrder(orderId: number, updateOrderRequest: UpdateOrderRequest): Observable<void>{
        return this.http.put<void>(`${this.baseUrl}/update/${orderId}`, updateOrderRequest);
    }

    getOrder(id: number): Observable<GetOrderResponse>{
        return this.http.get<GetOrderResponse>(`${this.baseUrl}/${id}`)
    }
}
