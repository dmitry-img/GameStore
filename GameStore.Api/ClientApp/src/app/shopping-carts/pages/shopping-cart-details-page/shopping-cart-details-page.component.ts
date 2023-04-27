import {Component, OnInit} from '@angular/core';
import {ShoppingCartService} from "../../../core/service/shopping-cart.service";
import {GetShoppingCartItemResponse} from "../../../core/models/GetShoppingCartItemResponse";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-shopping-cart-details-page',
  templateUrl: './shopping-cart-details-page.component.html',
  styleUrls: ['./shopping-cart-details-page.component.scss']
})
export class ShoppingCartDetailsPageComponent implements OnInit{
  items!: GetShoppingCartItemResponse[];
  constructor(private shoppingCartService: ShoppingCartService,
  private toaster: ToastrService) { }

  ngOnInit(): void {
    this.updateItems();
  }
  onDeleteItem(gameKey: string) {
    this.shoppingCartService.deleteItem(gameKey).subscribe(() => {
      this.updateItems();
    });
  }

  private updateItems(){
    this.shoppingCartService.getAllItems().subscribe((items: GetShoppingCartItemResponse[]) =>{
      this.items = items;
    })
  }
}
