import { MatSnackBar } from '@angular/material';
import { Injectable, NgZone } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    constructor(private snackBar: MatSnackBar,
        private zone: NgZone) { }

    public error(errors: string[]): void {
        this.zone.run(() => {
            const message = errors.join(" \r\n ");
            this.snackBar.open(message, '', {
                duration: 5000,
                verticalPosition: 'bottom',
                horizontalPosition: 'left',
                panelClass: ['mat-toolbar', 'mat-warn']
            });
        });
    }

    public success(message: string): void {
        this.zone.run(() => {
            this.snackBar.open(message, '', {
                duration: 5000,
                verticalPosition: 'bottom',
                horizontalPosition: 'left',
                panelClass: ['mat-toolbar', 'mat-accent']
            });
        });
    }

    public progress(message: string): void {
        this.zone.run(() => {
        this.snackBar.open(message, '', {
            verticalPosition: 'bottom',
            horizontalPosition: 'left',
            panelClass: ['mat-toolbar', 'mat-primary']
        });
    });
    }

    public dismiss(): void {
        this.snackBar.dismiss();
    }
}