import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {ActivatedRoute, Router} from "@angular/router";
import {GameService} from "../../services/game.service";
import {CommentService} from "../../services/comment.service";
import {ShoppingCartService} from "../../../shopping-carts/services/shopping-cart.service";
import {ToastrService} from "ngx-toastr";
import {HierarchicalDataService} from "../../../core/services/hierarchical-data.service";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {PublisherService} from "../../../publishers/services/publisher.service";
import {FormBuilder} from "@angular/forms";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {switchMap, tap} from "rxjs";
import {GetGameBriefResponse} from "../../models/GetGameBriefResponse";
import {AuthService} from "../../../auth/services/auth.service";

@Component({
  selector: 'app-game-list-management-page',
  templateUrl: './game-list-management-page.component.html',
  styleUrls: ['./game-list-management-page.component.scss']
})
export class GameListManagementPageComponent implements OnInit{
    @ViewChild('gameDeleteModalBody') gameDeleteModalBody!: TemplateRef<any>;
    paginatedGames!: PaginationResult<GetGameBriefResponse>;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;
    userRole!: string | null

    constructor(
        private gameService: GameService,
        private authService: AuthService,
        private fb: FormBuilder,
        private modalService: BsModalService,
        private toaster: ToastrService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.paginationRequest = {
            PageNumber: 1,
            PageSize: 10
        }

        this.getUserRole();
        this.getGamesOfCurrentPage()
    }

    getUserRole(): void{
        this.authService.getUserRole().subscribe((userRole: string | null) =>{
            this.userRole = userRole;
        })
    }

    onDelete(game: GetGameBriefResponse): void {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.gameDeleteModalBody
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.gameService.deleteGame(game.Key).subscribe(() =>{
                this.toaster.success(`The game has been deleted successfully!`);
                this.getGamesOfCurrentPage();
                this.bsModalRef.hide();
            });
        });
    }

    private getGamesOfCurrentPage(): void {
        this.route.paramMap.pipe(
            tap(params => {
                const pageNumber = +params.get('page')!;
                if (isNaN(pageNumber)) {
                    this.navigateToFirstPage()
                } else {
                    this.paginationRequest.PageNumber = pageNumber;
                }
            }),
            switchMap(() => this.userRole == 'Publisher'
                ? this.gameService.getCurrentPublisherGames(this.paginationRequest)
                : this.gameService.getGamesWithPagination(this.paginationRequest))
        ).subscribe((paginatedGames: PaginationResult<GetGameBriefResponse>) => {
            this.paginatedGames = paginatedGames;
        });
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/admin-panel/users/1']);
    }
}
