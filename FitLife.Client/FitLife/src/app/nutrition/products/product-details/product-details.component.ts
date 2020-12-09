import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { NumberValidators } from 'src/app/shared/validators/number.validator';
import { AddProductRequest } from '../models/add/addProduct.request';
import { AddProductResponse } from '../models/add/addProduct.response';
import { ProductsService } from '../services/products.service';
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
    onlyLettersRegex = '^[a-zA-Z\\s]*$'
  

  constructor( private fb:FormBuilder, private chartsService:ProductDetailsChartsService,
    private productsService: ProductsService, private notificationService: NotificationService,
    private router: Router) { 
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

  onSubmit():void{
    let request:AddProductRequest = {
      name: this.productForm.get('name').value,
      proteinsGrams: this.productForm.get('proteins').value,
      carbsGrams: this.productForm.get('carbohydrates').value,
      fatsGrams: this.productForm.get('fats').value,
    }
    this.productsService.addProduct(request).subscribe(response => this.handleResponse(response));
  }

  private handleResponse(response: AddProductResponse){
    if (!response.success) {
      this.notificationService.error(response.errors);
    }
    else {
      this.notificationService.success('Product succesfully created!');
      this.router.navigate(['/nutrition'], { queryParams: { tab: "Products" } });
    }
  }
}

export class ChartItem{
  name:string;
  value:number;
}
