import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Router} from '@angular/router';
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        const expectedRoles = route.data['expectedRoles'];
        const decodedToken = this.authService.decodeAccessToken();

        if (!decodedToken) {
            this.router.navigate(['/']);
            return false;
        }

        if (expectedRoles && Array.isArray(expectedRoles) && !expectedRoles.includes(decodedToken.Role)) {
            this.router.navigate(['/']);
            return false;
        }

        return true;
    }
}
