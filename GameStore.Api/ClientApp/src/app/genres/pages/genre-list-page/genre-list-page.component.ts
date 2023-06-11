import {Component, TemplateRef, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";
import {GenreService} from "../../services/genre.service";
import {GetGenreResponse} from "../../models/GetGenreResponse";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {PaginationRequest} from "../../../shared/models/PaginationRequest";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {map, Observable, switchMap, tap} from "rxjs";
import {GetUserResponse} from "../../../admin-panel/models/GetUserResponse";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-genre-list-page',
  templateUrl: './genre-list-page.component.html',
  styleUrls: ['./genre-list-page.component.scss']
})
export class GenreListPageComponent {
    @ViewChild('genreFormBody') genreFormBody!: TemplateRef<any>;
    @ViewChild('genreDeleteModalBody') genreDeleteModalBody!: TemplateRef<any>;
    paginatedGenres!: PaginationResult<GetGenreResponse>;
    genresForDropDown!: DropDownItem[];
    genreForm!: FormGroup;
    bsModalRef!: BsModalRef;
    paginationRequest!: PaginationRequest
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

        this.getGenresOfCurrentPage();
    }

    onModify(genre: GetGenreResponse) {
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


    onDelete(genre: GetGenreResponse) {
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

    onCreate() {
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
        this.route.paramMap.pipe(
            tap(params => {
                const pageNumber = +params.get('page')!;
                if (isNaN(pageNumber)) {
                    this.navigateToFirstPage()
                } else {
                    this.paginationRequest.PageNumber = pageNumber;
                }
            }),
            switchMap(() => this.genreService.getAllGenresWithPagination(this.paginationRequest))
        ).subscribe((paginatedGenres: PaginationResult<GetGenreResponse>) => {
            this.paginatedGenres = paginatedGenres;
        });
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/genre/list/1']);
    }
}
