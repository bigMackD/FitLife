import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NumberValidators } from 'src/app/shared/validators/number.validator';
import { MacrosChartCategories, ProductDetailsChartsService } from './product-details-chart.service';
import { NumberSumValidator } from './validators/numberSum.validator';
@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.sass']
})
export class ProductDetailsComponent implements OnInit {

  productForm: FormGroup;
  chartData: ChartItem[] = [];
  view: any[] = [700, 400];
    colorScheme = {
      domain: ['#9B26AF', '#691A99', '#68EFAD', '#AAAAAA']
    };
    onlyLettersRegex = '[a-zA-Z]+'
  

  constructor( private fb:FormBuilder, private chartsService:ProductDetailsChartsService) { 
  }

  ngOnInit() {
    this.chartsService.createChartData();
    this.chartData = this.chartsService.chartData;
    this.constructForm();
    this.subscribeChartToFormChanges();
  }

  private constructForm(): void {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern(this.onlyLettersRegex)]],
      proteins: ['', [Validators.required, NumberValidators.range(0,100)]],
      carbohydrates: ['', [Validators.required, NumberValidators.range(0,100)]],
      fats: ['', [Validators.required, NumberValidators.range(0,100)]],
    }, {validator: NumberSumValidator.inRange});
  }

  private subscribeChartToFormChanges():void{
    this.productForm.get('proteins').valueChanges.subscribe(data => { 
      this.chartsService.editChartValue(MacrosChartCategories[MacrosChartCategories.Proteins], Number(data))
          this.chartData = [...this.chartsService.chartData];
    });
    this.productForm.get('carbohydrates').valueChanges.subscribe(data => { 
      this.chartsService.editChartValue(MacrosChartCategories[MacrosChartCategories.Carbohydrates], Number(data))
          this.chartData = [...this.chartsService.chartData];
    });
    this.productForm.get('fats').valueChanges.subscribe(data => { 
      this.chartsService.editChartValue(MacrosChartCategories[MacrosChartCategories.Fats], Number(data))
          this.chartData = [...this.chartsService.chartData];
    });
  }

}

export class ChartItem{
  name:string;
  value:number;
}
