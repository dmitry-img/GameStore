import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
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
import {BehaviorSubject, Subject, switchMap, takeUntil, tap} from "rxjs";
import {GetGameBriefResponse} from "../../models/GetGameBriefResponse";
import {AuthService} from "../../../auth/services/auth.service";

@Component({
  selector: 'app-game-list-management-page',
  templateUrl: './game-list-management-page.component.html',
  styleUrls: ['./game-list-management-page.component.scss']
})
export class GameListManagementPageComponent implements OnInit, OnDestroy {
    @ViewChild('gameDeleteModalBody') gameDeleteModalBody!: TemplateRef<any>;
    paginatedGames!: PaginationResult<GetGameBriefResponse>;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;
    userRole!: string | null
    private pageNumber$ = new BehaviorSubject<number>(1);
    private paginatedGames$ = new BehaviorSubject<PaginationResult<GetGameBriefResponse>|null>(null);
    private destroy$ = new Subject<void>();


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
        this.subscribeToPageNumber();
        this.subscribeToPaginatedGames();
        this.subscribeToRouteParams();
    }

    getUserRole(): void {
        this.authService.getUserRole$()
            .pipe(takeUntil(this.destroy$))
            .subscribe(userRole => this.userRole = userRole);
    }

    onDelete(game: GetGameBriefResponse): void {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.gameDeleteModalBody
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.pipe(takeUntil(this.destroy$)).subscribe(() => {
            this.gameService.deleteGame(game.Key).subscribe(() =>{
                this.toaster.success(`The game has been deleted successfully!`);
                this.getGamesOfCurrentPage();
                this.bsModalRef.hide();
            });
        });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    private subscribeToPageNumber(): void {
        this.pageNumber$
            .pipe(
                tap(pageNumber => {
                    if (isNaN(pageNumber)) {
                        this.navigateToFirstPage();
                    } else {
                        this.paginationRequest.PageNumber = pageNumber;
                    }
                }),
                switchMap(() => this.userRole == 'Publisher'
                    ? this.gameService.getCurrentPublisherGames(this.paginationRequest)
                    : this.gameService.getGamesWithPagination(this.paginationRequest)),
                takeUntil(this.destroy$)
            )
            .subscribe(this.paginatedGames$);
    }

    private subscribeToPaginatedGames(): void {
        this.paginatedGames$
            .pipe(takeUntil(this.destroy$))
            .subscribe((paginatedGames: PaginationResult<GetGameBriefResponse> | null) => {
                if (paginatedGames !== null) {
                    this.paginatedGames = paginatedGames;
                }
            });
    }

    private subscribeToRouteParams(): void {
        this.route.paramMap
            .pipe(takeUntil(this.destroy$))
            .subscribe(params => {
                const pageNumber = +params.get('page')!;
                this.pageNumber$.next(pageNumber);
            });
    }

    private getGamesOfCurrentPage(): void {
        this.pageNumber$.next(this.paginationRequest.PageNumber);
    }

    private navigateToFirstPage(): void {
        this.router.navigate(['/admin-panel/users/1']);
    }
}
