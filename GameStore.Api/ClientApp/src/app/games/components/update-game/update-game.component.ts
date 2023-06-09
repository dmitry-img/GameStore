import {Component, ElementRef, Input, ViewChild} from '@angular/core';
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {GameService} from "../../services/game.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {GetGameResponse} from "../../models/GetGameResponse";
import {UpdateGameRequest} from "../../models/UpdateGameRequest";
import {throwError} from "rxjs";

@Component({
  selector: 'app-update-game',
  templateUrl: './update-game.component.html',
  styleUrls: ['./update-game.component.scss']
})
export class UpdateGameComponent {
    @ViewChild("parentGenreRef") parentGenreRef!: ElementRef<HTMLInputElement>;
    @Input() genres!: CheckboxListItem[];
    @Input() platformTypes!: CheckboxListItem[];
    @Input() publishers!: DropDownItem[];
    @Input() game!: GetGameResponse;
    @Input() discontinuedOptions!: DropDownItem[];
    @Input() userRole!: string | null;

    updateGameForm!: FormGroup

    constructor(
        private fb: FormBuilder,
        private gameService: GameService,
        private toaster: ToastrService,
    ) { }

    ngOnInit(): void {
        this.updateGameForm = this.fb.group({
            Name: [this.game.Name, Validators.required],
            Description: [this.game.Description, [Validators.required, Validators.minLength(50)]],
            GenreIds: [this.game.Genres.map(g => g.Id)],
            PlatformTypeIds: [this.game.PlatformTypes.map(pt => pt.Id)],
            PublisherId: [this.game.Publisher ? this.game.Publisher.Id : null],
            Price: [this.game.Price, [Validators.required, Validators.min(0.01)]],
            UnitsInStock: [this.game.UnitsInStock, [Validators.required, Validators.min(1)]],
            Discontinued: [this.game.Discontinued, Validators.required]
        });
    }

    onSubmit(): void {
        if (this.updateGameForm.invalid) {
            return;
        }

        this.gameService.updateGame(this.game.Key, this.updateGameForm.value).subscribe(() => {
            this.toaster.success("The game has been updated successfully!")
        });
    }
}
