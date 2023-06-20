import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {ToastrService} from "ngx-toastr";
import {ActivatedRoute, Router} from "@angular/router";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {BehaviorSubject, Subject, switchMap, takeUntil, tap} from "rxjs";
import {GetPublisherBriefResponse} from "../../models/GetPublisherBriefResponse";
import {PublisherService} from "../../services/publisher.service";

@Component({
  selector: 'app-publisher-list-page',
  templateUrl: './publisher-list-page.component.html',
  styleUrls: ['./publisher-list-page.component.scss']
})
export class PublisherListPageComponent implements OnInit, OnDestroy{
    @ViewChild('publisherDeleteModalBody') roleDeleteModalBody!: TemplateRef<any>;
    paginatedPublishers!: PaginationResult<GetPublisherBriefResponse>;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;
    private pageNumber$ = new BehaviorSubject<number>(1);
    private paginatedPublishers$ = new BehaviorSubject<PaginationResult<GetPublisherBriefResponse>|null>(null);
    private destroy$ = new Subject<void>();

    constructor(
        private publisherService: PublisherService,
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

        this.subscribeToPageNumber();
        this.subscribeToPaginatedPublishers();
        this.subscribeToRouteParams();
    }

    onDelete(publisher: GetPublisherBriefResponse) {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.roleDeleteModalBody
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.publisherService.deletePublisher(publisher.Id).subscribe(() =>{
                this.toaster.success(`The publisher has been deleted successfully!`);
                this.getPublishersOfCurrentPage()
                this.bsModalRef.hide();
            });
        });
    }

    private subscribeToPageNumber(): void {
        this.pageNumber$
            .pipe(
                takeUntil(this.destroy$),
                tap(pageNumber => {
                    if (isNaN(pageNumber)) {
                        this.navigateToFirstPage()
                    } else {
                        this.paginationRequest.PageNumber = pageNumber;
                    }
                }),
                switchMap(() => this.publisherService.getAllPublishersBriefWithPagination(this.paginationRequest))
            )
            .subscribe(this.paginatedPublishers$);
    }

    private subscribeToPaginatedPublishers(): void {
        this.paginatedPublishers$
            .pipe(takeUntil(this.destroy$))
            .subscribe((paginatedPublishers: PaginationResult<GetPublisherBriefResponse> | null) => {
                if (paginatedPublishers !== null) {
                    this.paginatedPublishers = paginatedPublishers;
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

    private navigateToFirstPage(): void {
        this.router.navigate(['/admin-panel/users/1']);
    }

    private getPublishersOfCurrentPage(){
        this.pageNumber$.next(this.paginationRequest.PageNumber);
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }
}
