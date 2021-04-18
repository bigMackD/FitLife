import { Injectable } from '@angular/core';
import { MacrosChartCategories } from 'src/app/nutrition/products/product-details/product-details-chart.service';
import { CategoryEnum } from '../enums/category.enum';
import { UserMeal } from '../models/get-user-meals/get-user-meals.response';

@Injectable({
    providedIn: 'root'
})

export class DashboardChartService {
  chartData: StackedHorizontalChartItem[] = [];
  seriesItems:StackedHorizontalChartSeriesItem[] =
  [{name: MacrosChartCategories[MacrosChartCategories.Carbohydrates], value:0},
  {name: MacrosChartCategories[MacrosChartCategories.Fats], value:0},
  {name: MacrosChartCategories[MacrosChartCategories.Proteins], value:0}];

  createChartData(): void {
    this.chartData = [
      {
        name: CategoryEnum[CategoryEnum.Breakfast],
        series: [...this.seriesItems]
      },
      {
        name: CategoryEnum[CategoryEnum.Lunch],
        series: [...this.seriesItems]
      },
      {
        name: CategoryEnum[CategoryEnum.Dinner],
        series: [...this.seriesItems]
      },
      {
        name: CategoryEnum[CategoryEnum.Snacks],
        series: [...this.seriesItems]
      }
    ];
  }

  updateChartData( breakfastMeals:UserMeal[]
    ,dinnerMeals:UserMeal[]
    ,lunchMeals:UserMeal[]
    ,snackMeals:UserMeal[]){
    
      this.chartData[0].series = [...this.updateChartSeriesItem( breakfastMeals)]
      this.chartData[1].series = [...this.updateChartSeriesItem( lunchMeals)]
      this.chartData[2].series = [...this.updateChartSeriesItem( dinnerMeals)]
      this.chartData[3].series = [...this.updateChartSeriesItem( snackMeals)]

    
  }

  updateChartSeriesItem(categoryMeals:UserMeal[]): StackedHorizontalChartSeriesItem[]{
    let carbsSum: number = 0;
    let proteinsSum: number = 0;  
    let fatsSum: number = 0;  

    categoryMeals.forEach(x => {
      carbsSum += x.carbsGrams;
      fatsSum += x.fatsGrams
      proteinsSum += x.proteinsGrams
      })
      let seriesItems:StackedHorizontalChartSeriesItem[] =
      [{name: MacrosChartCategories[MacrosChartCategories.Carbohydrates], value:carbsSum},
      {name: MacrosChartCategories[MacrosChartCategories.Fats], value:fatsSum},
      {name: MacrosChartCategories[MacrosChartCategories.Proteins], value:proteinsSum}];
      return seriesItems;
  }
}


export class StackedHorizontalChartItem{
    name:string
    series: StackedHorizontalChartSeriesItem[]
}

export class StackedHorizontalChartSeriesItem{
    name:string;
    value: number;
}
