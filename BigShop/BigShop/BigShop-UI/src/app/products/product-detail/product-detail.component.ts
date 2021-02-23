import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Category } from 'src/app/categories/category.model';
import { CategoryService } from 'src/app/categories/category.service';
import { Manufacturer } from 'src/app/manufacturers/manufacturer.model';
import { ManufacturerService } from 'src/app/manufacturers/manufacturer.service';
import { Product } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product: Product;
  id: number;
  manufacturer: Manufacturer;
  category: Category;

  constructor(private productService: ProductService, private categoryService: CategoryService, private manufacturerService: ManufacturerService , private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.product = this.productService.getProduct(this.id);
        this.manufacturer = this.manufacturerService.getManufacturer(this.product.manufacturer_Id);
        this.category = this.categoryService.getCategory(this.product.category_Id);
      }
    );
  }

  onEditProduct() {
    this.router.navigate(['edit'], {relativeTo: this.route});
  }

  onDeleteProduct() {
    this.productService.deleteProduct(this.id);
    this.router.navigate(['/products']);
  }
}
