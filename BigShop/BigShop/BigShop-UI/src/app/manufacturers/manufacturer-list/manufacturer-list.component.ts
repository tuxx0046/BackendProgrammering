import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Manufacturer } from '../manufacturer.model';
import { ManufacturerService } from '../manufacturer.service';

@Component({
  selector: 'app-manufacturer-list',
  templateUrl: './manufacturer-list.component.html',
  styleUrls: ['./manufacturer-list.component.css']
})
export class ManufacturerListComponent implements OnInit {
  manufacturers: Manufacturer[];

  constructor(private manufacturerService: ManufacturerService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.manufacturers = this.manufacturerService.getManufacturers();
  }

  onNewManufacturer() {
    this.router.navigate(['new'], {relativeTo: this.route});
  }

}
