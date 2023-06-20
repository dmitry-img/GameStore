import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {BanDuration} from "../../models/BanDuration";
import {CommentService} from "../../services/comment.service";
import {ToastrService} from "ngx-toastr";
import {UserService} from "../../../admin-panel/services/user.service";

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
        private userService: UserService,
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
            {Id: BanDuration.OneHour, Value: 'One Hour'},
            {Id: BanDuration.OneDay, Value: 'One Day'},
            {Id: BanDuration.OneWeek, Value: 'One Week'},
            {Id: BanDuration.OneMonth, Value: 'One Month'},
            {Id: BanDuration.Permanent, Value: 'Permanent'},
        ]
    }

    onBan(banDuration: BanDuration): void {
        this.userService.ban({
            BanDuration: banDuration,
            CommentId: this.commentId
        }).subscribe(() => {
            this.toaster.success("User has been successfully banned!");
            this.router.navigate(['/']);
        });
    }
}
