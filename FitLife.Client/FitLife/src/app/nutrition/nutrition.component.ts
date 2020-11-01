import { Component, OnInit } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material';
import { NutritionTabEnum } from './enums/tab.enum';

@Component({
  selector: 'app-nutrition',
  templateUrl: './nutrition.component.html',
  styleUrls: ['./nutrition.component.sass']
})
export class NutritionComponent implements OnInit {
  
  nutritionTabEnum = NutritionTabEnum;
  currentTabIndex: number = 0;
  constructor() { }

  ngOnInit() {
  }

  tabChanged(tabChangeEvent: MatTabChangeEvent): void {
    this.currentTabIndex = tabChangeEvent.index;
    console.log('index => ', this.nutritionTabEnum[tabChangeEvent.index]);
  }

}
