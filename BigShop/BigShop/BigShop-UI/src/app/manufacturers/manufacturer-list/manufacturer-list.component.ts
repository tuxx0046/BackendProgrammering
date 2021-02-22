import { Component, OnInit} from '@angular/core';
import { Manufacturer } from '../manufacturer.model';
import { ManufacturerService } from '../manufacturer.service';

@Component({
  selector: 'app-manufacturer-list',
  templateUrl: './manufacturer-list.component.html',
  styleUrls: ['./manufacturer-list.component.css']
})
export class ManufacturerListComponent implements OnInit {
  manufacturers: Manufacturer[];

  constructor(private manufacturerService: ManufacturerService) { }

  ngOnInit(): void {
    this.manufacturers = this.manufacturerService.getManufacturers();
  }


}
