import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {GetRoleResponse} from "../../models/GetRoleResponse";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {RoleService} from "../../services/role.service";
import {ToastrService} from "ngx-toastr";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {GetUserResponse} from "../../models/GetUserResponse";
import {UserService} from "../../services/user.service";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {ActivatedRoute, Router} from "@angular/router";
import {BehaviorSubject, Subject, switchMap, takeUntil, tap} from "rxjs";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetGameResponse} from "../../../games/models/GetGameResponse";

@Component({
  selector: 'app-user-list-page',
  templateUrl: './user-list-page.component.html',
  styleUrls: ['./user-list-page.component.scss']
})
export class UserListPageComponent implements OnInit, OnDestroy{
    @ViewChild('createUserFormBody') userFormBody!: TemplateRef<any>;
    @ViewChild('updateUserFormBody') updateFormBody!: TemplateRef<any>;
    @ViewChild('userDeleteModalBody') roleDeleteModalBody!: TemplateRef<any>;
    paginatedUsers!: PaginationResult<GetUserResponse>;
    roles!: DropDownItem[];
    createUserForm!: FormGroup;
    updateUserForm!: FormGroup;
    isUpdating: boolean = false;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;
    private pageNumber$ = new BehaviorSubject<number>(1);
    private paginatedUsers$ = new BehaviorSubject<PaginationResult<GetUserResponse>|null>(null);
    private destroy$ = new Subject<void>();

    constructor(
        private userService: UserService,
        private roleService: RoleService,
        private fb: FormBuilder,
        private modalService: BsModalService,
        private toaster: ToastrService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.createUserForm = this.fb.group({
            'Email': ['', [Validators.required, Validators.email]],
            'Username': ['', [
                Validators.required,
                Validators.minLength(6),
                Validators.pattern('^[a-zA-Z0-9_-]*$')
            ]],
            'RoleId': [null, Validators.required],
            'Password': ['', [Validators.required, Validators.minLength(6)]],
            'ConfirmPassword': ['', [Validators.required, Validators.minLength(6)]]
        });

        this.updateUserForm = this.fb.group({
            'Username': ['', [
                Validators.required,
                Validators.minLength(6),
                Validators.pattern('^[a-zA-Z0-9_-]*$')
            ]],
            'RoleId': [null, Validators.required]
        })

        this.paginationRequest = {
            PageNumber: 1,
            PageSize: 10
        }

        this.subscribeToPageNumber();
        this.subscribeToPaginatedUsers();
        this.subscribeToRouteParams();

        this.getRoles();
    }

    getRoles() {
        this.roleService.getAllRoles().subscribe((roles: GetRoleResponse[]) =>{
            this.roles = roles.map((role: GetRoleResponse) => ({
                Id: role.Id,
                Value: role.Name
            }));
        })
    }


    onModify(user: GetUserResponse) {
        const initialState = {
            title: 'Update the user',
            btnOkText: 'Update',
            btnCancelText: 'Cancel',
            content: this.updateFormBody
        };

        this.updateUserForm.patchValue({
            'Username': user.Username,
            'RoleId': user.Role ? user.Role.Id : null
        });

        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            if (this.updateUserForm.valid) {
                this.userService.updateUser(user.ObjectId, this.updateUserForm.value).subscribe(() => {
                    this.toaster.success(`The user has been modified successfully!`);
                    this.getUsersOfCurrentPage();
                    this.bsModalRef.hide();
                });
            }
        });
    }

    onDelete(user: GetUserResponse) {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.roleDeleteModalBody
        };

        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.userService.deleteUser(user.ObjectId).subscribe(() =>{
                this.toaster.success(`The user has been deleted successfully!`);
                this.getUsersOfCurrentPage();
                this.bsModalRef.hide();
            });
        });
    }

    onCreate() {
        const initialState = {
            title: 'Create a new user',
            btnOkText: 'Create',
            btnCancelText: 'Cancel',
            content: this.userFormBody
        };

        this.createUserForm.reset({
            'RoleId': null
        })

        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.createUserForm.markAllAsTouched();

            if (this.createUserForm.valid) {
                this.userService.createUser(this.createUserForm.value).subscribe(() => {
                    this.toaster.success(`The user has been created successfully!`);
                    this.getUsersOfCurrentPage();
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
                switchMap(() => this.userService.getAllUsersWithPagination(this.paginationRequest)),
                takeUntil(this.destroy$)
            )
            .subscribe(this.paginatedUsers$);
    }

    private subscribeToPaginatedUsers(): void {
        this.paginatedUsers$
            .pipe(takeUntil(this.destroy$))
            .subscribe((paginatedUsers: PaginationResult<GetUserResponse> | null) => {
                if (paginatedUsers !== null) {
                    this.paginatedUsers = paginatedUsers;
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

    private getUsersOfCurrentPage(): void {
        this.pageNumber$.next(this.paginationRequest.PageNumber);
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/admin-panel/users/1']);
    }
}
