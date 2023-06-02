import { Component } from '@angular/core';
import {RegistrationRequest} from "../../models/RegistrationRequest";
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {AuthResponse} from "../../models/AuthResponse";
import {Router} from "@angular/router";
import {compareValidator} from "../../../games/validators/compare.validator";
import {equalValidator} from "../../validators/equal.validator";

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.scss']
})
export class RegistrationPageComponent {
    registerForm!: FormGroup;

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit(): void {
        this.registerForm = new FormGroup({
            Email: new FormControl('', [Validators.required, Validators.email]),
            Username: new FormControl('', Validators.required),
            Password: new FormControl('', Validators.required),
            ConfirmPassword: new FormControl('', Validators.required),
        }, { validators: equalValidator<string>('Password', 'ConfirmPassword', String) });
    }

    onSubmit() {
        if (this.registerForm.valid) {
            this.authService.register(this.registerForm.value).subscribe(() => {
                this.authService.login({
                    Username: this.registerForm.get("Username")?.value,
                    Password: this.registerForm.get("Password")?.value
                }).subscribe((response: AuthResponse) => {
                    this.authService.saveTokens(response);
                    this.router.navigate(['/'])
                })
            });
        }
    }
}
