import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Category } from 'src/app/categories/category.model';
import { CategoryService } from 'src/app/categories/category.service';
import { Manufacturer } from 'src/app/manufacturers/manufacturer.model';
import { ManufacturerService } from 'src/app/manufacturers/manufacturer.service';
import { Product } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  id: number;
  editMode = false;
  manufacturers: Manufacturer[];
  categories: Category[];
  currentCategory: number ;
  currentManufacturer: number;
  selectedManufacturer: number;
  selectedCategory: number;
  productForm: FormGroup;

  constructor(private route: ActivatedRoute, private manufacturerService: ManufacturerService, private categoryService: CategoryService, private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        // console.log(this.editMode);
        this.initForm();
      }
    );

    this.manufacturers = this.manufacturerService.getManufacturers();
    this.categories = this.categoryService.getCategories();
  }

  onSubmit() {
    // console.log(this.productForm.value);
    const newProduct = new Product(
      this.productForm.value['name'],
      this.productForm.value['price'],
      this.productForm.value['ean'],
      this.productForm.value['weightGram'],
      this.selectedManufacturer,
      this.selectedCategory
      );

      if (this.editMode) {
        newProduct.id = this.id;
        this.productService.updateProduct(this.id, newProduct);
      }
      else {
        this.productService.addProduct(newProduct);
      }
      this.onCancel();
  }

  onCancel() {
    this.router.navigate(['../'], {relativeTo: this.route});
  }

  private initForm() {
    let productName = '';
    let productPrice;
    let productEAN = '';
    let productWeight;

    if (this.editMode) {
      const product = this.productService.getProduct(this.id);
      productName = product.name;
      productPrice = product.price;
      productEAN = product.ean;
      productWeight = product.weightGram;
      this.currentManufacturer = product.manufacturer_Id;
      this.currentCategory = product.category_Id;
      this.selectedManufacturer = this.currentCategory;
      this.selectedCategory = this.currentCategory;
    }

    this.productForm = new FormGroup({
      'name': new FormControl(
          productName, 
          [Validators.required, 
          Validators.minLength(5), 
          Validators.maxLength(150)]),
      'price': new FormControl(
          productPrice, 
          [Validators.required, 
          Validators.min(0), 
          Validators.max(9999999.99)]),
      'ean': new FormControl(
          productEAN, 
          [Validators.required, 
          Validators.minLength(13), 
          Validators.maxLength(13)]), 
      'weightGram': new FormControl(
          productWeight, 
          [Validators.required, 
          Validators.min(0), 
          Validators.max(9999999)]),
      'manufacturer_Id': new FormControl(),
      'category_Id': new FormControl()
    });
  }

  onManufacturerChange(manufacturerId: number) {    
    this.selectedManufacturer = manufacturerId;
  }

  onCategoryChange(categoryId: number) {
    this.selectedCategory = categoryId;
  }
}
