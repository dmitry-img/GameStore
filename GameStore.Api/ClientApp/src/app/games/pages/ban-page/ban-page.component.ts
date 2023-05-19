import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {BanDuration} from "../../models/BanDuration";

@Component({
  selector: 'app-ban-page',
  templateUrl: './ban-page.component.html',
  styleUrls: ['./ban-page.component.scss']
})
export class BanPageComponent implements OnInit{
    commentId!: number;
    banDurations!: DropDownItem[]
    constructor(private route: ActivatedRoute) { }

    ngOnInit(): void {
       this.getData();
    }

    private getData(): void{
        this.route.params.subscribe((data) =>{
            this.commentId = data['commentId'];
        })

        this.banDurations = [
            {id: BanDuration.OneDay, value: 'One Day'},
            {id: BanDuration.OneWeek, value: 'One Week'},
            {id: BanDuration.OneMonth, value: 'One Month'},
            {id: BanDuration.Permanent, value: 'Permanent'},
        ]
    }

    onBan() {

    }
}
