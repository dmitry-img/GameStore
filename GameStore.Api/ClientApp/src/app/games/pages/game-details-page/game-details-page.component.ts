import {Component, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {GetGameResponse} from "../../models/GetGameResponse";
import {GameService} from "../../../core/services/game.service";
import {CommentService} from "../../services/comment.service";
import {GetCommentResponse} from "../../models/GetCommentResponse";
import {ToastrService} from "ngx-toastr";
import {Subscription} from "rxjs";
import {CommentNode} from "../../models/CommentNode";
import {ShoppingCartService} from "../../../core/services/shopping-cart.service";
import {CreateShoppingCartItemRequest} from "../../../shopping-carts/models/CreateShoppingCartItemRequest";
import {HierarchicalDataService} from "../../../core/services/hierarchical-data.service";

@Component({
    selector: 'app-game-details-page',
    templateUrl: './game-details-page.component.html',
    styleUrls: ['./game-details-page.component.scss']
})
export class GameDetailsPageComponent implements OnInit, OnDestroy {
    game!: GetGameResponse
    commentNodes!: CommentNode[]
    newCommentSubscription!: Subscription;
    gameKey!: string;

    constructor(
        private route: ActivatedRoute,
        private gameService: GameService,
        private commentService: CommentService,
        private shoppingCartService: ShoppingCartService,
        private toaster: ToastrService,
        private hierarchicalDataService: HierarchicalDataService
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(data => {
            this.getGame(data['key'])
            this.getCommentListArray(data['key'])
            this.gameKey = data['key'];
        });
        this.onCommentCreated()
    }

    onDownloadGame(): void {
        this.gameService.downloadGame(this.gameKey, this.game.Name);
    }

    onBuyGame(game: GetGameResponse): void {
        const cartItem: CreateShoppingCartItemRequest = {
            GameKey: game.Key,
            GameName: game.Name,
            GamePrice: game.Price,
            Quantity: 1
        }
        this.shoppingCartService.addItem(cartItem).subscribe(() => {
            this.toaster.success(`${game.Name} has been added to shopping cart!`)
        });
    }

    onCommentCreated(): void {
        this.newCommentSubscription = this.commentService.getEmittedComment$().subscribe(() => {
            this.getCommentListArray(this.game.Key);
        });
    }

    ngOnDestroy(): void {
        this.newCommentSubscription.unsubscribe();
    }

    private getCommentListArray(gameKey: string): void {
        this.commentService.getCommentList(gameKey).subscribe((comments: GetCommentResponse[]) => {
            this.commentNodes = this.hierarchicalDataService.convertToTreeStructure<GetCommentResponse, CommentNode>(
                comments,
                'Id',
                'ParentCommentId',
                (comment) => ({comment})
            );
        });
    }

    private getGame(key: string): void {
        this.gameService.getGameByKey(key).subscribe((game: GetGameResponse) => {
                this.game = game;
            }
        )
    }
}
