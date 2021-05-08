import { AbstractControl, AsyncValidatorFn, ValidationErrors, ValidatorFn } from "@angular/forms";
import { Observable, of } from "rxjs";
import { delay, map } from "rxjs/operators";

function checkIfFileNameExists(fileName: string, fileNameList: string[]): Observable<boolean> {
    return of(fileNameList.includes(fileName));
}
export function forbiddenEndingValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        var temp = control.value?.slice(control.value?.lastIndexOf("."), control.value?.length);
        var forbidden = false;
        if (temp == ".cs" || temp == ".java" || temp == ".py" || temp == ".cpp" || temp == ".c")
            forbidden = true;
        return forbidden ? { fileNameEnding: true } : null;
    };
}
export function forbiddenNameValidator(fileNameList: string[]): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
        return checkIfFileNameExists(control.value, fileNameList).pipe(
            map(res => {
                return res ? { fileNameExists: true } : null;
            })
        );
    };
}