import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Manufacturer } from '../manufacturer.model';
import { ManufacturerService } from '../manufacturer.service';

@Component({
  selector: 'app-manufacturer-detail',
  templateUrl: './manufacturer-detail.component.html',
  styleUrls: ['./manufacturer-detail.component.css']
})
export class ManufacturerDetailComponent implements OnInit {
  manufacturer: Manufacturer;
  id: number;
  constructor(private manufacturerService: ManufacturerService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.manufacturer = this.manufacturerService.getManufacturer(this.id);
      }
    )
  }

  onEditManufacturer() {
    this.router.navigate(['edit'], {relativeTo: this.route});
  }

}
