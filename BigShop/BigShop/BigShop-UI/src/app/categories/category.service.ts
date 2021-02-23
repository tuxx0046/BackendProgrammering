import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Category } from "./category.model";

@Injectable({providedIn: 'root'})
export class CategoryService {
    categoriesChanged = new Subject<Category[]>();

    constructor(private http: HttpClient) {}

    private categories: Category[] = [
        new Category('TV', 0),
        new Category('Telefon', 1)
      ];

    getCategories() {
        return this.categories.slice();
    }

    getCategory(id: number): Category {
        let category = this.categories.find(c => c.id == id);

        return this.categories.find(c => c.id == id);
    }

    addCategory(category: Category) {
        let nextIndex = this.categories.length;
        category.id = nextIndex;
        this.categories.push(category);

        this.categoriesChanged.next(this.categories.slice());
    }

    updateCategory(id: number, newCategory: Category) {
        let category = this.categories.find(c => c.id == id)
        let index = this.categories.indexOf(category);
        this.categories[index] = newCategory;

        this.categoriesChanged.next(this.categories.slice());
    }

    deleteCategory(id: number) {
        let category = this.categories.find(c => c.id == id)
        let index = this.categories.indexOf(category);
        this.categories.splice(index, 1);

        this.categoriesChanged.next(this.categories.slice());
    }
}