import { Component, Input, OnInit} from '@angular/core';
import { Manufacturer } from '../../manufacturer.model';

@Component({
  selector: 'app-manufacturer-item',
  templateUrl: './manufacturer-item.component.html',
  styleUrls: ['./manufacturer-item.component.css']
})
export class ManufacturerItemComponent implements OnInit {
  @Input() manufacturer: Manufacturer;
  @Input() index: number;

  ngOnInit(): void {
    
  }


}
