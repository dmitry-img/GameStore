import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {AbstractControl, FormControl} from "@angular/forms";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-validation-message',
  templateUrl: './validation-message.component.html',
  styleUrls: ['./validation-message.component.scss']
})
export class ValidationMessageComponent{
  @Input() control!: AbstractControl;
  @Input() errorMessages!: { [key: string]: string };
}
