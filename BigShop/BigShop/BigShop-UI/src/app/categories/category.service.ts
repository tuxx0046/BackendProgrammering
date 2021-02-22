import { EventEmitter } from "@angular/core";
import { Category } from "./category.model";

export class CategoryService {
    categorySelected = new EventEmitter<Category>();
    
    private categories: Category[] = [
        new Category('TV', 0),
        new Category('Telefon', 1)
      ];

    getCategories() {
        return this.categories.slice();
    }
}