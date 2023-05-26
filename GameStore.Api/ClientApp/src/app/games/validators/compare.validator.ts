import { AbstractControl, ValidatorFn } from '@angular/forms';

export function compareValidator<T>(controlName1: string, controlName2: string, parseFn: (value: any) => T): ValidatorFn {
    return (control: AbstractControl): {[key: string]: any} | null => {
        const control1 = control.get(controlName1);
        const control2 = control.get(controlName2);

        if (!control1?.value || !control2?.value) {
            return null;
        }

        const value1 = parseFn(control1.value);
        const value2 = parseFn(control2.value);

        return value1 > value2 ? { 'comparisonInvalid': true } : null;
    };
}
