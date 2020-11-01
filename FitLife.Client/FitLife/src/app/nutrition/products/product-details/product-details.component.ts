import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.sass']
})
export class ProductDetailsComponent implements OnInit {

  productForm: FormGroup;

  constructor( private fb:FormBuilder) { }

  ngOnInit() {
    this.constructForm();
  }

  private constructForm(): void {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      proteins: ['', [Validators.required]],
      carbohydrates: ['', [Validators.required]],
      fats: ['', [Validators.required]],
     
    });
  }

}
