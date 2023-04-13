import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
  
export class ProgressBarService {
    public isVisible: boolean = false;

    public show():void{
        this.isVisible = true;
    }

    public hide():void{
        this.isVisible = false;
    }
}