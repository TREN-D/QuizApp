import { Directive, Input } from '@angular/core';
import { Validator, AbstractControl, ValidationErrors, NG_VALIDATORS, ValidatorFn, FormControl } from '@angular/forms';

export function compareValidator(controlNameTocompare: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control instanceof FormControl) {
      if (control.value === null || control.value.length === 0) {
        return null;
      }
      const constrolToCompare = control.root.get(controlNameTocompare);
      if (constrolToCompare) {
        const subscription = constrolToCompare.valueChanges.subscribe(() => {
          control.updateValueAndValidity();
          subscription.unsubscribe();
        });
      }
      return constrolToCompare && constrolToCompare.value !== control.value ? { compare: true } : null;
    } else {
      // This else statement return null if we are using compare directive to form or formGroup instead of formControl
      return null;
    }
  };
}
@Directive({
  selector: '[appPasswordCompare]',
  providers: [{ provide: NG_VALIDATORS, useExisting: PasswordComparerDirective, multi: true }]
})
export class PasswordComparerDirective implements Validator {
  @Input() controlNameTocompare: string;

  constructor() {
  }

  validate(control: AbstractControl): ValidationErrors | null {
    return compareValidator(this.controlNameTocompare)(control);
  }
}
