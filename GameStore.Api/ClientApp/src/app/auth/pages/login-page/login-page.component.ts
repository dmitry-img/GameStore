import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {AuthResponse} from "../../models/AuthResponse";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {
    loginForm!: FormGroup;
    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
    ) { }

    ngOnInit(): void {
        this.loginForm = this.fb.group({
            Username: ['', Validators.required],
            Password: ['', Validators.required],
        });
    }

    onSubmit(): void {
        if(this.loginForm.valid){
            this.authService.login(this.loginForm.value).subscribe((authResponse: AuthResponse) =>{
                this.authService.saveTokens(authResponse);
                this.router.navigate(['/'])
            });
        }
    }
}
