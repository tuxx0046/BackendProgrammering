import { Component, OnInit } from '@angular/core';
import { Manufacturer } from './manufacturer.model';
import { ManufacturerService } from './manufacturer.service';

@Component({
  selector: 'app-manufacturers',
  templateUrl: './manufacturers.component.html',
  styleUrls: ['./manufacturers.component.css']
})
export class ManufacturersComponent implements OnInit {
  selectedManufacturer: Manufacturer;

  constructor(private manufacturerService: ManufacturerService) { }

  ngOnInit() {
    this.manufacturerService.manufacturerSelected.subscribe(
      (manufacturer: Manufacturer) => {
        this.selectedManufacturer = manufacturer;
      }
    )
  }

}
