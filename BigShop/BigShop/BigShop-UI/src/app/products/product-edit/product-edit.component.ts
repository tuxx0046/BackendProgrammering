import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Category } from 'src/app/categories/category.model';
import { CategoryService } from 'src/app/categories/category.service';
import { Manufacturer } from 'src/app/manufacturers/manufacturer.model';
import { ManufacturerService } from 'src/app/manufacturers/manufacturer.service';

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
  defaultCategory = 1;
  defaultManufacturer = 1;

  constructor(private route: ActivatedRoute, private manufacturerService: ManufacturerService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        // console.log(this.editMode);
      }
    );

    this.manufacturers = this.manufacturerService.getManufacturers();
    this.categories = this.categoryService.getCategories();
    
  }

}
