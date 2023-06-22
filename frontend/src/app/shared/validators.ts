import { AbstractControl } from '@angular/forms'



export function ConfirmPassword(password : AbstractControl, confirm_password: AbstractControl){
    if( password.value != confirm_password.value ){
        return { 'DoNotMatch' : true };
    }
    return null;
}