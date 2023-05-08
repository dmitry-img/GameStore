import {Pipe, PipeTransform} from '@angular/core';
import {AbstractControl, FormControl} from "@angular/forms";

@Pipe({
    name: 'formControl'
})
export class FormControlPipe implements PipeTransform {
    transform(formGroup: AbstractControl, controlName: string): FormControl {
        return formGroup.get(controlName) as FormControl;
    }
}
