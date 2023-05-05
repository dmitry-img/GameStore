import {Component, Input, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.scss']
})
export class InputFieldComponent {
  @Input() control!: FormControl;
  @Input() isRequired!: boolean;
  @Input() labelText!: string;
  @Input() type: string = 'text';
}
