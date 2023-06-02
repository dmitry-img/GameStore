import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {GetRoleResponse} from "../../models/GetRoleResponse";
import {RoleService} from "../../services/role.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {switchMap, tap} from "rxjs";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetUserResponse} from "../../models/GetUserResponse";
import {ActivatedRoute, Router} from "@angular/router";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";

@Component({
  selector: 'app-role-list-page',
  templateUrl: './role-list-page.component.html',
  styleUrls: ['./role-list-page.component.scss']
})
export class RoleListPageComponent implements OnInit{
    @ViewChild('roleFormBody') roleFormBody!: TemplateRef<any>;
    @ViewChild('roleDeleteModalBody') roleDeleteModalBody!: TemplateRef<any>;
    paginatedRoles!: PaginationResult<GetRoleResponse>;
    roleForm!: FormGroup;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;

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

        this.getRolesOfCurrentPage()
    }

    onDelete(role: GetRoleResponse) {
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
            });

            this.bsModalRef.hide();
        });
    }

    onCreate() {
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
                this.roleService.createRole(this.roleForm.value).subscribe({
                    next: () => {
                        this.toaster.success(`The role has been created successfully!`);
                        this.getRolesOfCurrentPage()
                        this.bsModalRef.hide();
                    },
                    error: (err) => {
                        this.toaster.error(err.error);
                    }
                });
            }
        });
    }

    private getRolesOfCurrentPage(): void {
        this.route.paramMap.pipe(
            tap(params => {
                const pageNumber = +params.get('page')!;
                if (isNaN(pageNumber)) {
                    this.navigateToFirstPage()
                } else {
                    this.paginationRequest.PageNumber = pageNumber;
                }
            }),
            switchMap(() => this.roleService.getAllRolesWithPagination(this.paginationRequest))
        ).subscribe((paginatedRoles: PaginationResult<GetRoleResponse>) => {
            this.paginatedRoles = paginatedRoles;
        });
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/admin-panel/users/1']);
    }
}
