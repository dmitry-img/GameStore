import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {GetRoleResponse} from "../../models/GetRoleResponse";
import {RoleService} from "../../services/role.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {BehaviorSubject, Subject, switchMap, takeUntil, tap} from "rxjs";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetUserResponse} from "../../models/GetUserResponse";
import {ActivatedRoute, Router} from "@angular/router";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";

@Component({
  selector: 'app-role-list-page',
  templateUrl: './role-list-page.component.html',
  styleUrls: ['./role-list-page.component.scss']
})
export class RoleListPageComponent implements OnInit, OnDestroy{
    @ViewChild('roleFormBody') roleFormBody!: TemplateRef<any>;
    @ViewChild('roleDeleteModalBody') roleDeleteModalBody!: TemplateRef<any>;
    paginatedRoles!: PaginationResult<GetRoleResponse>;
    roleForm!: FormGroup;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;
    private pageNumber$ = new BehaviorSubject<number>(1);
    private paginatedRoles$ = new BehaviorSubject<PaginationResult<GetRoleResponse>|null>(null);
    private destroy$ = new Subject<void>();

    constructor(
        private roleService: RoleService,
        private fb: FormBuilder,
        private modalService: BsModalService,
        private toaster: ToastrService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.roleForm = this.fb.group({
            'Name': ['', Validators.required]
        })

        this.paginationRequest = {
            PageNumber: 1,
            PageSize: 10
        }

        this.subscribeToPageNumber();
        this.subscribeToPaginatedRoles();
        this.subscribeToRouteParams();
    }

    onDelete(role: GetRoleResponse): void {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.roleDeleteModalBody
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.roleService.deleteRole(role.Id).subscribe(() =>{
                this.toaster.success(`The role has been deleted successfully!`);
                this.getRolesOfCurrentPage()
                this.bsModalRef.hide();
            });
        });
    }

    onCreate(): void {
        const initialState = {
            title: 'Create a new role',
            btnOkText: 'Create',
            btnCancelText: 'Cancel',
            content: this.roleFormBody
        };
        this.roleForm.get('Name')?.reset();
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.roleForm.markAllAsTouched();

            if(this.roleForm.valid){
                this.roleService.createRole(this.roleForm.value).subscribe(() => {
                    this.toaster.success(`The role has been created successfully!`);
                    this.getRolesOfCurrentPage()
                    this.bsModalRef.hide();
                });
            }
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
                switchMap(() => this.roleService.getAllRolesWithPagination(this.paginationRequest)),
                takeUntil(this.destroy$)
            )
            .subscribe(this.paginatedRoles$);
    }

    private subscribeToPaginatedRoles(): void {
        this.paginatedRoles$
            .pipe(takeUntil(this.destroy$))
            .subscribe((paginatedRoles: PaginationResult<GetRoleResponse> | null) => {
                if (paginatedRoles !== null) {
                    this.paginatedRoles = paginatedRoles;
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

    private getRolesOfCurrentPage(): void {
        this.pageNumber$.next(this.paginationRequest.PageNumber);
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/admin-panel/users/1']);
    }
}
