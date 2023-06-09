import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {GameService} from "../../services/game.service";

@Component({
    selector: 'app-create-game',
    templateUrl: './create-game.component.html',
    styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {
    @ViewChild("parentGenreRef") parentGenreRef!: ElementRef<HTMLInputElement>;
    @Input() genres!: CheckboxListItem[];
    @Input() platformTypes!: CheckboxListItem[];
    @Input() publishers!: DropDownItem[];
    createGameForm!: FormGroup

    constructor(
        private fb: FormBuilder,
        private gameService: GameService,
        private toaster: ToastrService,
        private router: Router,
    ) { }

    ngOnInit(): void {
        this.createGameForm = this.fb.group({
            Name: ['', Validators.required],
            Description: ['', [Validators.required, Validators.minLength(50)]],
            GenreIds: [this.fb.array([])],
            PlatformTypeIds: [this.fb.array([]), Validators.required],
            PublisherId: [null],
            Price: ['', [Validators.required, Validators.min(0.01)]],
            UnitsInStock: ['', [Validators.required, Validators.min(1)]]
        });
    }

    onSubmit(): void {
        if (this.createGameForm.invalid) {
            return;
        }

        this.gameService.createGame(this.createGameForm.value).subscribe(() => {
            this.toaster.success("The game has been created successfully!")
            this.router.navigate(['/game/list/management/'])
        });
    }
}
