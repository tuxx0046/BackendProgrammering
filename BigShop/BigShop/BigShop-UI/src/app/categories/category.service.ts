import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Category } from "./category.model";

@Injectable({providedIn: 'root'})
export class CategoryService {
    categoriesChanged = new Subject<Category[]>();

    constructor(private http: HttpClient) {}

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
                // console.log(response);
                this.loadCategories();
            }
        );
    }
}