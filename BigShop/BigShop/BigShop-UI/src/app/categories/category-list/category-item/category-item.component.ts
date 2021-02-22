import { Component, Input, OnInit} from '@angular/core';

import { Category } from '../../category.model';
import { CategoryService } from '../../category.service';

@Component({
  selector: 'app-category-item',
  templateUrl: './category-item.component.html',
  styleUrls: ['./category-item.component.css']
})
export class CategoryItemComponent implements OnInit {
  @Input() category: Category;

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
  }

  onSelected() {
    this.categoryService.categorySelected.emit(this.category);
  }
}
