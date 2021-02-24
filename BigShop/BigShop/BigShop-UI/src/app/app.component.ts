import { Component, OnInit } from '@angular/core';
import { CategoryService } from './categories/category.service';
import { ManufacturerService } from './manufacturers/manufacturer.service';
import { ProductService } from './products/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private categoryService: CategoryService, private manufacturerService: ManufacturerService, private productService: ProductService){}

  ngOnInit(){
    this.categoryService.getCategories();
    this.manufacturerService.getManufacturers();
  }
}
