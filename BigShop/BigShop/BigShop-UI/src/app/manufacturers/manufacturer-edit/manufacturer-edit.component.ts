import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Manufacturer } from '../manufacturer.model';
import { ManufacturerService } from '../manufacturer.service';

@Component({
  selector: 'app-manufacturer-edit',
  templateUrl: './manufacturer-edit.component.html',
  styleUrls: ['./manufacturer-edit.component.css']
})
export class ManufacturerEditComponent implements OnInit {
  id: number;
  editMode = false;
  manufacturerForm: FormGroup;

  constructor(private route: ActivatedRoute, private manufacturerService: ManufacturerService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        // console.log(this.editMode);
        this.initForm();
      }
    )
  }

  onSubmit() {
    const newManufacturer = new Manufacturer(this.manufacturerForm.value['name']);

    if (this.editMode) {
      newManufacturer.id = this.id;
      this.manufacturerService.updateManufacturer(this.id, newManufacturer);
    }
    else {
      this.manufacturerService.addManufacturer(newManufacturer);
    }
    this.onCancel();
  }

  onCancel() {
    this.router.navigate(['../'], {relativeTo: this.route});
  }

  private initForm() {
    let manufacturerName = '';

    if (this.editMode) {
      const category = this.manufacturerService.getManufacturer(this.id);
      manufacturerName = category.name
    }    

    this.manufacturerForm = new FormGroup({
      'name': new FormControl(manufacturerName, [Validators.required, Validators.minLength(1), Validators.maxLength(100)])
    });
  }

}
