import { Injectable } from '@angular/core';
import {CreateGameRequest} from "../../games/models/CreateGameRequest";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {GetOrderResponse} from "../models/GetOrderResponse";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
    private baseUrl = '/api/payments';
    constructor(private http: HttpClient) { }

    payByVisa(orderId: number): Observable<number>{
        return this.http.post<number>(`${this.baseUrl}/visa`, orderId);
    }
    payByIbox(orderId: number): Observable<number> {
        return this.http.post<number>(`${this.baseUrl}/ibox`, orderId);
    }

    payByBank(orderId: number) : void {
        this.http.post(`${this.baseUrl}/bank`, orderId, {responseType: 'blob'})
            .subscribe((response: any) => {
                const blob = new Blob([response], {type: response.type});
                const downloadUrl = URL.createObjectURL(blob);
                const link = document.createElement('a');
                link.href = downloadUrl;
                link.download = `GameStoreInvoice${orderId}.txt`;
                link.click();
            });
    }

}
