import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MealCategoriesResponse, MealCategory } from '../../mealCategories/models/list/getMealCategories.response';
import { Product, ProductsResponse } from '../../products/models/list/products.response';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.sass']
})
export class MealDetailsComponent implements OnInit {
  mealCategories: MealCategory[];
  products: Product[];

  constructor(private activatedRoute: ActivatedRoute) { }
  
  ngOnInit() {
  this.getDropdownsData();
  }

  private getDropdownsData():void{
    let mealCategoriesdata = this.activatedRoute.snapshot.data.mealCategories as MealCategoriesResponse;
    let productsData = this.activatedRoute.snapshot.data.products as ProductsResponse;
    this.mealCategories = mealCategoriesdata.mealCategories;
    this.products = productsData.products;
  }
}
