import { FormControl, FormGroup } from '@angular/forms';

export class NumberSumValidator {
    static inRange(group: FormGroup) {
        let sum = Number(group.controls['fats'].value) + Number(group.controls['proteins'].value) 
        + Number(group.controls['carbohydrates'].value);
        return sum > 100 ? { sumRangeNotValid: true} : null
    }
}