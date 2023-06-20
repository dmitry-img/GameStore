import {Directive, Input, TemplateRef, ViewContainerRef} from '@angular/core';
import {AuthService} from "../../auth/services/auth.service";

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective {
    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private authService: AuthService
    ) { }

    @Input() set appHasRole(roles: string[]) {
        this.authService.getUserRole$().subscribe((userRole: string | null) => {
            if(userRole === null || !roles.includes(userRole)){
                this.viewContainer.clear();
                return;
            }
            this.viewContainer.createEmbeddedView(this.templateRef);
        });
    }
}
