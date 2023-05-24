import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetShoppingCartItemResponse} from "../models/GetShoppingCartItemResponse";
import {CreateShoppingCartItemRequest} from "../models/CreateShoppingCartItemRequest";

@Injectable({
    providedIn: 'root'
})
export class ShoppingCartService {
    private baseUrl = '/api/shopping-carts';

    constructor(private http: HttpClient) { }

    getAllItems(): Observable<GetShoppingCartItemResponse[]> {
        return this.http.get<GetShoppingCartItemResponse[]>(`${this.baseUrl}/items`);
    }

    getQuantity(gameKey: string): Observable<number> {
        return this.http.get<number>(`${this.baseUrl}/quantity/${gameKey}`);
    }

    addItem(newItem: CreateShoppingCartItemRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/add-item`, newItem);
    }

    deleteItem(gameKey: string): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/delete-item/${gameKey}`);
    }
}
