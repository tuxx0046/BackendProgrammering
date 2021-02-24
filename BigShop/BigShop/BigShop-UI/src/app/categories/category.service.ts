import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { ProductService } from "../products/product.service";
import { Category } from "./category.model";

@Injectable({providedIn: 'root'})
export class CategoryService {
    categoriesChanged = new Subject<Category[]>();
    // errorEvent = new Subject<string>();

    constructor(private http: HttpClient, private productService: ProductService) {}

    private categories: Category[] = [
        // new Category('TV', 0),
        // new Category('Telefon', 1)
      ];

    setCategories(categories: Category[]) {
        this.categories = categories;
        this.categoriesChanged.next(this.categories.slice());
    }

    loadCategories() {
        this.http.get<Category[]>('http://localhost:5000/api/categories').subscribe(
            categories => {
                this.setCategories(categories);
            }
        );
    }

    getCategories() {
        this.loadCategories();
        return this.categories.slice();
    }

    getCategory(id: number): Category {
        let category = this.categories.find(c => c.id == id);

        return this.categories.find(c => c.id == id);
    }

    addCategory(category: Category) {
        this.http.post<Category>('http://localhost:5000/api/categories', category).subscribe(
            category => {
                this.categories.push(category);
                this.loadCategories();
            }
        );
    }

    updateCategory(id: number, newCategory: Category) {
        let index = this.categories.indexOf(this.categories.find(c => c.id == id))
        this.categories[index] = newCategory;

        const updateUrl = 'http://localhost:5000/api/categories/' + id;
        this.http.put(updateUrl, newCategory).subscribe(
            response => {
                this.loadCategories();
            }
        );
    }

    deleteCategory(id: number) {
        const deleteUrl = 'http://localhost:5000/api/categories/' + id;
        this.http.delete(deleteUrl).subscribe(
            response => {                
                this.loadCategories();
            }, error => {
                alert('Cannot delete category with products in it');
                // this.errorEvent.next(error.message);
            }
        );
    }
}