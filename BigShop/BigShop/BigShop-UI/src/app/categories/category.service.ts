import { Subject } from "rxjs";
import { Category } from "./category.model";

export class CategoryService {
    
    private categories: Category[] = [
        new Category('TV', 0),
        new Category('Telefon', 1)
      ];

    getCategories() {
        return this.categories.slice();
    }

    getCategory(id: number) {
        return this.categories[id];
    }
}