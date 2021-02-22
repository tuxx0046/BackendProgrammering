import { Component, Input, OnInit} from '@angular/core';
import { Manufacturer } from '../../manufacturer.model';
import { ManufacturerService } from '../../manufacturer.service';

@Component({
  selector: 'app-manufacturer-item',
  templateUrl: './manufacturer-item.component.html',
  styleUrls: ['./manufacturer-item.component.css']
})
export class ManufacturerItemComponent implements OnInit {
  @Input() manufacturer: Manufacturer;

  constructor(private manufacturerService: ManufacturerService) { }

  ngOnInit(): void {
    
  }

  onSelected() {
    this.manufacturerService.manufacturerSelected.emit(this.manufacturer);
  }

}
