import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { CategoryStartComponent } from '../category-start/category-start.component';

import { Category } from '../category.model';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent implements OnInit {
  category: Category;
  id: number;

  constructor(private categoryService: CategoryService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.category = this.categoryService.getCategory(this.id);
      }
    )
  }

  onEditCategory() {
    this.router.navigate(['edit'], {relativeTo: this.route});
  }

}
