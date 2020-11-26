import { Injectable } from '@angular/core';
import { ChartItem } from 'src/app/nutrition/products/product-details/product-details.component';

@Injectable({
    providedIn: 'root'
})

export class ChartsService{
    createChart(){
        var chartData:ChartItem[] = [
            {
              name: "Proteins",
              value: 0
            },
            {
              name: "Carbohydrates",
              value: 0
            },
            {
              name: "Fats",
              value: 0
            }
          ]; 
    }
}