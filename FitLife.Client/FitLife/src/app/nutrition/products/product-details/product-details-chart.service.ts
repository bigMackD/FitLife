import { Injectable } from '@angular/core';
import { ChartItem } from 'src/app/nutrition/products/product-details/product-details.component';

@Injectable({
    providedIn: 'root'
})

export class ProductDetailsChartsService {
  chartData: ChartItem[] = [];

  createChartData(): void {
    this.chartData = [
      {
        name: MacrosChartCategories[MacrosChartCategories.Proteins],
        value: 0
      },
      {
        name: MacrosChartCategories[MacrosChartCategories.Carbohydrates],
        value: 0
      },
      {
        name: MacrosChartCategories[MacrosChartCategories.Fats],
        value: 0
      }
    ];
  }

    editChartValue(name:string, value:number):void{
      if(value <= 100 && value >= 0){
        let itemIndex = this.chartData.findIndex(x => x.name == name);
        this.chartData[itemIndex].value = value;
      }
    }
}

export enum MacrosChartCategories{
  Proteins = 0,
  Carbohydrates = 1,
  Fats = 2
}