import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DateFilterOption} from "../../models/DateFilterOption";
import {SortOption} from "../../models/SortOption";
import {FilterGameRequest} from "../../models/FilterGameRequest";
import {compareValidator} from "../../validators/compare.validator";
import {DropDownItem} from "../../../shared/models/DropDownItem";

@Component({
  selector: 'app-game-list-sidebar',
  templateUrl: './game-list-sidebar.component.html',
  styleUrls: ['./game-list-sidebar.component.scss']
})
export class GameListSidebarComponent implements OnInit{
    filterGameForm!: FormGroup;
    @Input() genres!: CheckboxListItem[];
    @Input() platformTypes!: CheckboxListItem[];
    @Input() publishers!: CheckboxListItem[];
    @Input() sortOptions!: DropDownItem[]
    @Input() dateFilterOption!: DropDownItem[]
    @Input() pageSizes!: DropDownItem[]
    @Output() filter = new EventEmitter<FilterGameRequest>();

    constructor(
        private fb: FormBuilder,
    ) { }

    ngOnInit(): void {
        this.filterGameForm = this.fb.group({
            NameFragment: [null, Validators.minLength(3)],

            GenreIds: [null],

            PlatformTypeIds: [null],

            PublisherIds: [null],

            PriceFrom: [null, Validators.min(1)],

            PriceTo: [null, Validators.min(1)],

            DateFilterOption: [DateFilterOption.None],

            SortOption: [SortOption.MostViewed],

            PageSize: [10]
        }, { validators: compareValidator<number>('PriceFrom', 'PriceTo', Number) });

    }

    onSubmit(): void {
        this.emitFormValueIfValid();
    }

    onChangePageSize(): void {
        this.emitFormValueIfValid();
    }

    private emitFormValueIfValid(): void{
        if(this.filterGameForm.valid){
            this.filter.emit(this.filterGameForm.value);
        }
    }
}
