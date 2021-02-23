import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Category } from '../category.model';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {
  id: number;
  editMode = false;
  categoryForm: FormGroup;

  constructor(private route: ActivatedRoute, private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        // console.log(this.editMode);
        this.initForm();
      }
    );
    
  }

  onSubmit() { 
    const newCategory = new Category(this.categoryForm.value['name']);

    if (this.editMode) {
      newCategory.id = this.id
      this.categoryService.updateCategory(this.id, newCategory);
    }
    else {
      this.categoryService.addCategory(newCategory);
    }
    this.onCancel();   
  }

  onCancel() {
    this.router.navigate(['../'], {relativeTo: this.route});
  }

  private initForm() {
    let categoryName = '';

    if (this.editMode) {
      const category = this.categoryService.getCategory(this.id);
      categoryName = category.name;
    }

    this.categoryForm = new FormGroup({
      'name': new FormControl(categoryName, [Validators.required, Validators.minLength(5), Validators.maxLength(50)])
    });
  }

}
