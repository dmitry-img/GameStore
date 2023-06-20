import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {GenreService} from "../../services/genre.service";
import {GetGenreResponse} from "../../models/GetGenreResponse";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {BehaviorSubject, map, Observable, Subject, switchMap, takeUntil, tap} from "rxjs";
import {GetUserResponse} from "../../../admin-panel/models/GetUserResponse";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-genre-list-page',
  templateUrl: './genre-list-page.component.html',
  styleUrls: ['./genre-list-page.component.scss']
})
export class GenreListPageComponent implements OnInit, OnDestroy{
    @ViewChild('genreFormBody') genreFormBody!: TemplateRef<any>;
    @ViewChild('genreDeleteModalBody') genreDeleteModalBody!: TemplateRef<any>;
    paginatedGenres!: PaginationResult<GetGenreResponse>;
    genresForDropDown!: DropDownItem[];
    genreForm!: FormGroup;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest
    private pageNumber$ = new BehaviorSubject<number>(1);
    private paginatedGenres$ = new BehaviorSubject<PaginationResult<GetGenreResponse>|null>(null);
    private destroy$ = new Subject<void>();


    constructor(
        private genreService: GenreService,
        private fb: FormBuilder,
        private modalService: BsModalService,
        private toaster: ToastrService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.genreForm = this.fb.group({
            'Name': ['', Validators.required],
            'ParentGenreId': [null]
        })

        this.paginationRequest ={
            PageNumber: 1,
            PageSize: 10
        }

        this.subscribeToPageNumber();
        this.subscribeToPaginatedGenres();
        this.subscribeToRouteParams();
    }

    onModify(genre: GetGenreResponse): void {
        const initialState = {
            title: 'Update the genre',
            btnOkText: 'Update',
            btnCancelText: 'Cancel',
            content: this.genreFormBody
        };

        this.getGenresForDropDown(genre).subscribe((genresForDropDown: DropDownItem[]) =>{
            this.genresForDropDown = genresForDropDown;
            this.genreForm.reset({
                'Name': genre.Name,
                'ParentGenreId': genre.ParentGenreId
            });

            this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

            this.bsModalRef.content.confirm.subscribe(() => {
                if(this.genreForm.valid) {
                    this.genreService.updateGenre(genre.Id, this.genreForm.value).subscribe(() => {
                        this.toaster.success(`The genre has been modified successfully!`);
                        this.getGenresOfCurrentPage();
                        this.bsModalRef.hide();
                    });
                }
            });
        });
    }


    onDelete(genre: GetGenreResponse): void {
        const initialState = {
            title: 'Are you sure?',
            btnOkText: 'Confirm',
            btnCancelText: 'Cancel',
            content: this.genreDeleteModalBody
        };
        this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

        this.bsModalRef.content.confirm.subscribe(() => {
            this.genreService.deleteGenre(genre.Id).subscribe(() =>{
                this.toaster.success(`The genre has been deleted successfully!`);
                this.getGenresOfCurrentPage();
                this.bsModalRef.hide();
            });
        });
    }

    onCreate(): void {
        const initialState = {
            title: 'Create a new genre',
            btnOkText: 'Create',
            btnCancelText: 'Cancel',
            content: this.genreFormBody
        };

        this.getGenresForDropDown().subscribe((genresForDropDown: DropDownItem[]) =>{
            this.genresForDropDown = genresForDropDown;
            this.genreForm.reset({
                'Name': '',
                'ParentGenreId': null
            });

            this.bsModalRef = this.modalService.show(ConfirmationModalComponent, {initialState});

            this.bsModalRef.content.confirm.subscribe(() => {
                this.genreForm.markAllAsTouched();

                if(this.genreForm.valid){
                    this.genreService.createGenre(this.genreForm.value).subscribe(() => {
                        this.toaster.success(`The genre has been created successfully!`);
                        this.getGenresOfCurrentPage();
                        this.bsModalRef.hide();
                    });
                }
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
                switchMap(() => this.genreService.getAllGenresWithPagination(this.paginationRequest))
            )
            .subscribe(this.paginatedGenres$);
    }

    private subscribeToPaginatedGenres(): void {
        this.paginatedGenres$
            .pipe(takeUntil(this.destroy$))
            .subscribe((paginatedGenres: PaginationResult<GetGenreResponse> | null) => {
                if (paginatedGenres !== null) {
                    this.paginatedGenres = paginatedGenres;
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


    private getGenresForDropDown(genreToIgnore: GetGenreResponse | null = null): Observable<DropDownItem[]> {
        return this.genreService.getAllGenres().pipe(
            map((genres: GetGenreResponse[]) => {
                if (genreToIgnore !== null) {
                    genres = genres.filter(genre => genre.Id !== genreToIgnore.Id && genre.ParentGenreId !== genreToIgnore.Id);
                }

                let dropDownGenres: DropDownItem[] = genres.map(g => ({
                    Id: g.Id,
                    Value: g.Name
                }));

                dropDownGenres.unshift({ Id: null, Value: 'None' });
                return dropDownGenres;
            })
        );
    }

    private getGenresOfCurrentPage(): void {
        this.pageNumber$.next(this.paginationRequest.PageNumber);
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/genre/list/1']);
    }
}
