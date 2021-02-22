// import { NgModule } from "@angular/core";
// import { RouterModule, Routes } from "@angular/router";
// import { AppComponent } from "./app.component";
// import { CategoriesComponent } from "./categories/categories.component";
// import { CategoryDetailComponent } from "./categories/category-detail/category-detail.component";
// import { CategoryEditComponent } from "./categories/category-edit/category-edit.component";
// import { CategoryStartComponent } from "./categories/category-start/category-start.component";
// import { ProductsComponent } from "./products/products.component";

// const appRoutes: Routes = [
//     {path: '', component: AppComponent, pathMatch: 'full'},
//     {path: 'categories', component: CategoriesComponent, children: [
//         {path: '', component: CategoryStartComponent},
//         {path: 'new', component: CategoryEditComponent},
//         {path: ':id', component: CategoryDetailComponent},
//         {path: ':id/edit', component: CategoryEditComponent},
//     ]},
//     {path: 'products', component: ProductsComponent},
// ]

// @NgModule({
//     imports: [RouterModule.forRoot(appRoutes)],
//     exports: [RouterModule]
// })
// export class AppRoutingModule {}