import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailComponent } from './categories/category-detail/category-detail.component';
import { CategoryEditComponent } from './categories/category-edit/category-edit.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { CategoryItemComponent } from './categories/category-list/category-item/category-item.component';
import { CategoryStartComponent } from './categories/category-start/category-start.component';
import { ProductsComponent } from './products/products.component';
import { ProductDetailComponent } from './products/product-detail/product-detail.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductItemComponent } from './products/product-list/product-item/product-item.component';
import { ProductStartComponent } from './products/product-list/product-start/product-start.component';
import { ManufacturersComponent } from './manufacturers/manufacturers.component';
import { ManufacturerDetailComponent } from './manufacturers/manufacturer-detail/manufacturer-detail.component';
import { ManufacturerEditComponent } from './manufacturers/manufacturer-edit/manufacturer-edit.component';
import { ManufacturerListComponent } from './manufacturers/manufacturer-list/manufacturer-list.component';
import { ManufacturerItemComponent } from './manufacturers/manufacturer-list/manufacturer-item/manufacturer-item.component';
import { ManufacturerStartComponent } from './manufacturers/manufacturer-start/manufacturer-start.component';
import { CategoryService } from './categories/category.service';
import { ManufacturerService } from './manufacturers/manufacturer.service';
import { ProductService } from './products/product.service';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CategoriesComponent,
    CategoryDetailComponent,
    CategoryEditComponent,
    CategoryListComponent,
    CategoryItemComponent,
    CategoryStartComponent,
    ProductsComponent,
    ProductDetailComponent,
    ProductEditComponent,
    ProductListComponent,
    ProductItemComponent,
    ProductStartComponent,
    ManufacturersComponent,
    ManufacturerDetailComponent,
    ManufacturerEditComponent,
    ManufacturerListComponent,
    ManufacturerItemComponent,
    ManufacturerStartComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [CategoryService, ManufacturerService, ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
