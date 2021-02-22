import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CategoriesComponent } from "./categories/categories.component";
import { CategoryDetailComponent } from "./categories/category-detail/category-detail.component";
import { CategoryEditComponent } from "./categories/category-edit/category-edit.component";
import { CategoryStartComponent } from "./categories/category-start/category-start.component";
import { ManufacturerDetailComponent } from "./manufacturers/manufacturer-detail/manufacturer-detail.component";
import { ManufacturerEditComponent } from "./manufacturers/manufacturer-edit/manufacturer-edit.component";
import { ManufacturerStartComponent } from "./manufacturers/manufacturer-start/manufacturer-start.component";
import { ManufacturersComponent } from "./manufacturers/manufacturers.component";
import { ProductDetailComponent } from "./products/product-detail/product-detail.component";
import { ProductEditComponent } from "./products/product-edit/product-edit.component";
import { ProductStartComponent } from "./products/product-list/product-start/product-start.component";
import { ProductsComponent } from "./products/products.component";

const appRoutes: Routes = [
    {path: '', redirectTo: '/products', pathMatch: 'full'},
    {path: 'categories', component: CategoriesComponent, children: [
        {path: '', component: CategoryStartComponent},
        {path: 'new', component: CategoryEditComponent},
        {path: ':id', component: CategoryDetailComponent},
        {path: ':id/edit', component: CategoryEditComponent},
    ]},
    {path: 'manufacturers', component: ManufacturersComponent, children: [
        {path: '', component: ManufacturerStartComponent},
        {path: 'new', component: ManufacturerEditComponent},
        {path: ':id', component: ManufacturerDetailComponent},
        {path: ':id/edit', component: ManufacturerEditComponent},
    ]},
    {path: 'products', component: ProductsComponent, children: [
        {path: '', component: ProductStartComponent},
        {path: 'new', component: ProductEditComponent},
        {path: ':id', component: ProductDetailComponent},
        {path: ':id/edit', component: ProductEditComponent},
    ]}
]

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}