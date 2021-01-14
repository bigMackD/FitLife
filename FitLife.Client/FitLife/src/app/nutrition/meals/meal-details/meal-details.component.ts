import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../../shared/services/notification.service';
import { MealCategoriesResponse } from '../../mealCategories/models/list/getMealCategories.response';
import { MealProduct, Product, ProductsResponse } from '../../products/models/list/products.response';
import { AddMealProductRequestModel, AddMealRequest } from '../models/add/add-meal.request';
import { AddMealResponse } from '../models/add/add-meal.response';
import { MealDetailsRequest } from '../models/details/meal-details-request';
import { EditMealRequest } from '../models/edit/edit-meal.request';
import { EditMealResponse } from '../models/edit/edit-meal.response';
import { MealCategoryModel } from '../models/shared/mealCategory.model';
import { MealsService } from '../services/meals.service';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.sass']
})
export class MealDetailsComponent implements OnInit {
  mealCategories: MealCategoryModel[];
  products: MealProduct[];
  selectedProducts: MealProduct[] = [];
  selectedCategories: MealCategoryModel[] = [];

  isInEditMode: boolean = false;
  mealId: number;
  pageTitle: string = "Add new meal";

  mealForm: FormGroup;
  onlyLettersRegex = '^[a-zA-Z\\s]*$'

  constructor(private activatedRoute: ActivatedRoute, private fb: FormBuilder,
    private mealsService: MealsService, private notificationService: NotificationService,
    private router: Router) { }

  ngOnInit() {
    this.getDropdownsData();
    this.constructForm();
    this.setEditMode();
  }

  private constructForm(): void {
    this.mealForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern(this.onlyLettersRegex)]],
      category: ['', [Validators.required]],
      products: ['', [Validators.required]]
    });
  }

  private getDropdownsData(): void {
    let mealCategoriesdata = this.activatedRoute.snapshot.data.mealCategories as MealCategoriesResponse;
    let productsData = this.activatedRoute.snapshot.data.products as ProductsResponse;
    this.mealCategories = mealCategoriesdata.mealCategories;
    this.products = productsData.products.map(val => {
      let mealProduct: MealProduct = new MealProduct(val.id, val.name,val.calories,val.proteinsGrams,val.carbsGrams, val.fatsGrams, 0)
      return mealProduct;
    });;
    
  }

  validateDropdowns(): boolean {
    return this.mealForm.get('category').valid && this.mealForm.get('products').valid;
  }

  public onSubmit(): void {
    let mealProducts: AddMealProductRequestModel[] = this.selectedProducts.map(val => ({id:val.id,grams: val.grams}));

    if (this.isInEditMode) {
      let request: EditMealRequest = {
        id: this.mealId,
        name: this.mealForm.get('name').value,
        mealProducts: mealProducts,
        categoryId: this.selectedCategories[0].id
      }
      this.mealsService.edit(request).subscribe(response => this.handleEditResponse(response));
    }
    else {
      let request: AddMealRequest = {
        name: this.mealForm.get('name').value,
        mealProducts: mealProducts,
        categoryId: this.selectedCategories[0].id
      }
      this.mealsService.addMeal(request).subscribe(response => this.handleAddResponse(response));
    }

  }

  selectProduct($event:Product){
    let mealProduct: MealProduct = new MealProduct($event.id, $event.name,$event.calories,$event.proteinsGrams,$event.carbsGrams, $event.fatsGrams, 0)
    this.selectedProducts.push(mealProduct)
  }
  unselectProduct($event){
    let index = this.selectedProducts.findIndex(p => p.id === $event.value.id);
    this.selectedProducts.splice(index,1);
  }

  private handleAddResponse(response: AddMealResponse) {
    if (!response.success) {
      this.notificationService.error(response.errors);
    }
    else {
      this.notificationService.success('Meal succesfully created!');
      this.router.navigate(['/nutrition'], { queryParams: { tab: "Meals" } });
    }
  }

  private handleEditResponse(response: EditMealResponse) {
    if (!response.success) {
      this.notificationService.error(response.errors);
    }
    else {
      this.notificationService.success('Meal succesfully edited!');
      this.router.navigate(['/nutrition'], { queryParams: { tab: "Meals" } });
    }
  }

  private setEditMode() {
    const mealIdFromRoute = this.activatedRoute.snapshot.paramMap.get('id');
    if (mealIdFromRoute) {
      this.isInEditMode = true;
      this.mealId = +mealIdFromRoute;
      this.pageTitle = "Edit meal"
      let request: MealDetailsRequest = { id: +mealIdFromRoute }
      this.mealsService.getDetails(request).subscribe(response => {
        this.mealForm.get('name').setValue(response.meal.name);
        this.selectedProducts = response.meal.mealProducts.map(val => {
          let mealProduct: MealProduct = new MealProduct(val.productId, val.name,val.calories,val.proteinsGrams,
            val.carbsGrams, val.fatsGrams, val.grams)
          return mealProduct;
        });;
        this.mealForm.get('products').setValue(response.meal.mealProducts)
        this.selectedCategories[0] = response.meal.category;
        this.selectedCategories = [...this.selectedCategories];
      });
    }
  }
}
