import { Component, OnInit } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { NutritionTabEnum } from './enums/tab.enum';

@Component({
  selector: 'app-nutrition',
  templateUrl: './nutrition.component.html',
  styleUrls: ['./nutrition.component.sass']
})
export class NutritionComponent implements OnInit {
  
  nutritionTabEnum = NutritionTabEnum;
  currentTabIndex: number = 0;
  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.selectedIndexFromParam();
  }

  tabChanged(tabChangeEvent: MatTabChangeEvent): void {
    this.currentTabIndex = tabChangeEvent.index;
  }

  selectedIndexFromParam(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if (params.tab) {
        switch (params.tab) {
          case NutritionTabEnum[NutritionTabEnum.Meals]:
            this.currentTabIndex = NutritionTabEnum.Meals;
            break;
          case NutritionTabEnum[NutritionTabEnum.Products]:
            this.currentTabIndex = NutritionTabEnum.Products;
            break;
          default:
            break;
        }
      }
    });
  }
}
