import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {ToastrService} from "ngx-toastr";
import {ActivatedRoute, Router} from "@angular/router";
import {BehaviorSubject, Subject, switchMap, takeUntil, tap} from "rxjs";
import {GetOrderResponse} from "../../models/GetOrderResponse";
import {OrderService} from "../../services/order.service";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {OrderState} from "../../models/OrderState";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";

@Component({
  selector: 'app-order-list-page',
  templateUrl: './order-list-page.component.html',
  styleUrls: ['./order-list-page.component.scss']
})
export class OrderListPageComponent implements OnInit, OnDestroy{
    @ViewChild('orderChangeToShippedModalBody') orderChangeToShippedModalBody!: TemplateRef<any>;
    @ViewChild('roleDeleteModalBody') orderDeleteModalBody!: TemplateRef<any>;
    paginatedOrders!: PaginationResult<GetOrderResponse>;
    orderForm!: FormGroup;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;
    orderStates!: DropDownItem[]
    order!: GetOrderResponse;
    orderDetailsControls: FormControl[] = [];
    private pageNumber$ = new BehaviorSubject<number>(1);
    private paginatedOrders$ = new BehaviorSubject<PaginationResult<GetOrderResponse>|null>(null);
    private destroy$ = new Subject<void>();

    constructor(
        private orderService: OrderService,
        private fb: FormBuilder,
        private modalService: BsModalService,
        private toaster: ToastrService,
        private route: ActivatedRoute,
        private router: Router,
    ) { }

    ngOnInit(): void {
        this.orderForm = this.fb.group({
            'OrderState': [null, Validators.required],
            'OrderDetails': this.fb.array([])
        });

        this.orderStates = [
            { Id: OrderState.Unpaid, Value: "Unpaid" },
            { Id: OrderState.Paid, Value: "Paid" },
            { Id: OrderState.Shipped, Value: "Shipped" }
        ]

        this.paginationRequest = {
            PageNumber: 1,
            PageSize: 10
        }

        this.setOrderDetailsControls();

        this.subscribeToPageNumber();
        this.subscribeToPaginatedOrders();
        this.subscribeToRouteParams();
    }

    get orderDetails(): FormArray {
        return this.orderForm.get('OrderDetails') as FormArray;
    }

    setOrderDetailsControls(): void{
        this.orderDetailsControls = (this.orderForm.get('OrderDetails') as FormArray).controls as FormControl[];
    }

    onChangeState(order: GetOrderResponse): void{
        const initialState = {
            title: 'Modify order',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.orderChangeToShippedModalBody
        };

        this.order = order;

        this.populateOrderForm(order);

        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.orderService.updateOrder(order.Id, this.orderForm.value).subscribe(() =>{
                this.toaster.success("The order has been successfully updated!");
                this.getOrdersOfCurrentPage();
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
                takeUntil(this.destroy$),
                tap(pageNumber => {
                    if (isNaN(pageNumber)) {
                        this.navigateToFirstPage();
                    } else {
                        this.paginationRequest.PageNumber = pageNumber;
                    }
                }),
                switchMap(() => this.orderService.getAllOrdersWithPagination(this.paginationRequest))
            )
            .subscribe(this.paginatedOrders$);
    }

    private subscribeToPaginatedOrders(): void {
        this.paginatedOrders$
            .pipe(takeUntil(this.destroy$))
            .subscribe((paginatedOrders: PaginationResult<GetOrderResponse> | null) => {
                if (paginatedOrders !== null) {
                    this.paginatedOrders = paginatedOrders;
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

    private populateOrderForm(order: GetOrderResponse) {
        this.orderForm.patchValue({
            'OrderState': order.OrderState,
        });

        const orderDetailsArray = this.orderForm.get('OrderDetails') as FormArray;
        while (orderDetailsArray.length) {
            orderDetailsArray.removeAt(0);
        }

        for (const detail of order.OrderDetails) {
            const detailGroup = this.fb.group({
                'GameKey': detail.GameKey,
                'Quantity': [detail.Quantity, [Validators.required, Validators.min(1)]]
            });

            orderDetailsArray.push(detailGroup);
        }
        this.setOrderDetailsControls();
    }
    private getOrdersOfCurrentPage(): void {
        this.pageNumber$.next(this.paginationRequest.PageNumber);
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/admin-panel/users/1']);
    }
}
