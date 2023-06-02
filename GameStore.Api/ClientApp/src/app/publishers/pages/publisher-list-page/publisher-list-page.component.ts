import {Component, TemplateRef, ViewChild} from '@angular/core';
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {GetRoleResponse} from "../../../admin-panel/models/GetRoleResponse";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {RoleService} from "../../../admin-panel/services/role.service";
import {ToastrService} from "ngx-toastr";
import {ActivatedRoute, Router} from "@angular/router";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {switchMap, tap} from "rxjs";
import {GetPublisherBriefResponse} from "../../models/GetPublisherBriefResponse";
import {PublisherService} from "../../services/publisher.service";

@Component({
  selector: 'app-publisher-list-page',
  templateUrl: './publisher-list-page.component.html',
  styleUrls: ['./publisher-list-page.component.scss']
})
export class PublisherListPageComponent {
    @ViewChild('publisherDeleteModalBody') roleDeleteModalBody!: TemplateRef<any>;
    paginatedPublishers!: PaginationResult<GetPublisherBriefResponse>;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest;

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

        this.getPublishersOfCurrentPage()
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
            });

            this.bsModalRef.hide();
        });
    }

    private getPublishersOfCurrentPage(): void {
        this.route.paramMap.pipe(
            tap(params => {
                const pageNumber = +params.get('page')!;
                if (isNaN(pageNumber)) {
                    this.navigateToFirstPage()
                } else {
                    this.paginationRequest.PageNumber = pageNumber;
                }
            }),
            switchMap(() => this.publisherService.getAllPublishersBriefWithPagination(this.paginationRequest))
        ).subscribe((paginatedPublishers: PaginationResult<GetPublisherBriefResponse>) => {
            this.paginatedPublishers = paginatedPublishers;
        });
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/admin-panel/users/1']);
    }
}
