import {AfterViewInit, Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router, RouterEvent} from "@angular/router";
import {filter} from "rxjs";

@Component({
  selector: 'app-main-admin-panel-page',
  templateUrl: './main-admin-panel-page.component.html',
  styleUrls: ['./main-admin-panel-page.component.scss']
})
export class MainAdminPanelPageComponent implements OnInit, AfterViewInit {
    activeTab!: string;

    constructor(private router: Router) { }

    ngOnInit() {
        this.setActiveTab();
    }

    ngAfterViewInit() {
        this.router.events.pipe(
            filter(event => event instanceof NavigationEnd)
        ).subscribe(() => this.setActiveTab());
    }

    private setActiveTab() {
        this.activeTab = this.router.url.split('/')[2];
    }
}
