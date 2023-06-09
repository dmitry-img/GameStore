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

        if (!decodedToken || !expectedRoles.includes(decodedToken.Role)) {
            this.router.navigate(['/login']);
            return false;
        }

        return true;
    }
}
