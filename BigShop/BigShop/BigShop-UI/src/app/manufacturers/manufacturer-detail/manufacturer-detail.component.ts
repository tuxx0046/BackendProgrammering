import { Component, Input, OnInit } from '@angular/core';
import { Manufacturer } from '../manufacturer.model';

@Component({
  selector: 'app-manufacturer-detail',
  templateUrl: './manufacturer-detail.component.html',
  styleUrls: ['./manufacturer-detail.component.css']
})
export class ManufacturerDetailComponent implements OnInit {
  @Input() manufacturer: Manufacturer;
  constructor() { }

  ngOnInit(): void {
  }

}
