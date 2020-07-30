import { MatSnackBar } from '@angular/material';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class NotificationService{

    constructor(private snackBar: MatSnackBar) {}

    public error(errors:string[]):void{
        const message = errors.join(" \r\n ");
        this.snackBar.open(message,'', {
            duration: 5000,
            verticalPosition: 'top',
            horizontalPosition: 'right',
            panelClass: ['mat-toolbar', 'mat-warn']
          });
    }

    public success(message:string):void{
        this.snackBar.open(message,'', {
            duration: 5000,
            verticalPosition: 'top',
            horizontalPosition: 'right',
            panelClass: ['mat-toolbar', 'mat-accent']
          });
    }
}