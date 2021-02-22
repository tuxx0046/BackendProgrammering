import { Component, OnInit } from '@angular/core';
import { Category } from './category.model';
import { CategoryService } from './category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  selectedCategory: Category;
  
  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.categorySelected.subscribe(
      (category: Category) => {
        this.selectedCategory = category;
      }
    );
  }

}
