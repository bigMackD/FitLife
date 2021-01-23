import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit {
  viewDate = new Date();
  constructor() { }

  ngOnInit() {
  }

  nextDay(){
    this.incrementDay(1)
  }

  previousDay(){
   this.incrementDay(-1)

  }

  private incrementDay(delta: number): void {
    this.viewDate = new Date(
      this.viewDate.getFullYear(),
      this.viewDate.getMonth(),
      this.viewDate.getDate() + delta);
  }

}
