import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-logout-page',
  templateUrl: './logout-page.component.html',
  styleUrls: ['./logout-page.component.scss']
})
export class LogoutPageComponent implements OnInit{
    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit(): void {
        const userObjectId = this.authService.decodeAccessToken()?.UserObjectId;
        this.authService.logout(userObjectId!).subscribe(() =>{
            this.router.navigate(['/login']);
        });
    }
}
