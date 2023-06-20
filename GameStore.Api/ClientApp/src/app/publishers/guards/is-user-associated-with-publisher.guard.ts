import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {map, Observable} from 'rxjs';
import {PublisherService} from "../services/publisher.service";
import {AuthService} from "../../auth/services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class IsUserAssociatedWithPublisherGuard {

    constructor(
        private publisherService: PublisherService,
        private router: Router,
        private  authService: AuthService
    ) { }
    canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if(this.authService.decodeAccessToken()?.Role == 'Publisher'){
            const companyName = route.params['companyName'];

            return this.publisherService.isUserAssociatedWithPublisher(companyName).pipe(
                map(isAssociated => {
                    if (isAssociated) {
                        return true;
                    } else {
                        return this.router.parseUrl('/game/list/');
                    }
                })
            );
        }
        return true;
    }
}
