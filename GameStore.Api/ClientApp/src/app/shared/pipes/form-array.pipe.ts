import { Pipe, PipeTransform } from '@angular/core';
import {AbstractControl, FormArray, FormControl} from "@angular/forms";

@Pipe({
  name: 'formArray'
})
export class FormArrayPipe implements PipeTransform {
  transform(formGroup: AbstractControl, controlName: string): FormArray {
    return formGroup.get(controlName) as FormArray;
  }
}
