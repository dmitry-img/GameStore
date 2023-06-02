import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {BanDuration} from "../../models/BanDuration";
import {CommentService} from "../../services/comment.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-ban-page',
  templateUrl: './ban-page.component.html',
  styleUrls: ['./ban-page.component.scss']
})
export class BanPageComponent implements OnInit{
    commentId!: number;
    banDurations!: DropDownItem[]
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private commentService: CommentService,
        private toaster: ToastrService
    ) { }

    ngOnInit(): void {
       this.getData();
    }

    private getData(): void{
        this.route.params.subscribe((data) =>{
            this.commentId = data['commentId'];
        })

        this.banDurations = [
            {Id: BanDuration.OneDay, Value: 'One Day'},
            {Id: BanDuration.OneWeek, Value: 'One Week'},
            {Id: BanDuration.OneMonth, Value: 'One Month'},
            {Id: BanDuration.Permanent, Value: 'Permanent'},
        ]
    }

    onBan(banDuration: BanDuration): void {
        this.commentService.ban({
            BanDuration: banDuration,
            CommentId: this.commentId
        }).subscribe(() => {
            this.toaster.success("User has been successfully banned!");
            this.router.navigate(['/']);
        });
    }
}
