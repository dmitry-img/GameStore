import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {filter} from "rxjs";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
    title = 'ClientApp';
    showAd: any;

    constructor(private router: Router) { }

    ngOnInit(): void {
        this.router.events.pipe(
            filter((event): event is NavigationEnd => event instanceof NavigationEnd)
        ).subscribe((event) => {
            const pattern = /^\/game\/list\/\d+$/;
            this.showAd = !(pattern.test(event.url) || event.url == '/');
        });
    }
}
