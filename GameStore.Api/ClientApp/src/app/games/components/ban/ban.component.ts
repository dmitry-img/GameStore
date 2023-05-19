import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {FormBuilder, FormGroup} from "@angular/forms";
import {BanDuration} from "../../models/BanDuration";

@Component({
  selector: 'app-ban',
  templateUrl: './ban.component.html',
  styleUrls: ['./ban.component.scss']
})
export class BanComponent implements OnInit{
    @Input() banDurations!: DropDownItem[];
    @Output() ban: EventEmitter<BanDuration> = new EventEmitter<BanDuration>();
    banForm!: FormGroup

    constructor(private fb: FormBuilder) { }

    ngOnInit(): void {
        this.banForm = this.fb.group({
            banDuration: [BanDuration.OneDay]
        })
    }

    onBan(): void {
        this.ban.emit(this.banForm.value);
    }
}
