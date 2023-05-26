import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {GetOrderResponse} from "../models/GetOrderResponse";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
    private baseUrl = '/api/orders';
    constructor(private http: HttpClient) { }

    createOrder() : Observable<GetOrderResponse>{
        return this.http.post<GetOrderResponse>(`${this.baseUrl}/create`,null);
    }
}
