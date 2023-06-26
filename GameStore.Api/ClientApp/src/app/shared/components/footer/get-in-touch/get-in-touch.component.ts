import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-get-in-touch',
  templateUrl: './get-in-touch.component.html',
  styleUrls: ['./get-in-touch.component.scss']
})
export class GetInTouchComponent implements OnInit{
    title: string = 'get in touch'
    buttonText: string = 'submit';
    getInTouchForm!: FormGroup;

    constructor(private fb: FormBuilder) { }

    ngOnInit(): void {
        this.getInTouchForm = this.fb.group({
            'Name': ['', Validators.required],
            'Email': ['', [Validators.required, Validators.email]],
            'Message': ['', Validators.required]
        })
    }

    onSubmit(): void {
        if(this.getInTouchForm.valid){ }
    }
}
