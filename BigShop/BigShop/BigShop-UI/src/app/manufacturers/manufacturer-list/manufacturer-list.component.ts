import { Component, OnDestroy, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Manufacturer } from '../manufacturer.model';
import { ManufacturerService } from '../manufacturer.service';

@Component({
  selector: 'app-manufacturer-list',
  templateUrl: './manufacturer-list.component.html',
  styleUrls: ['./manufacturer-list.component.css']
})
export class ManufacturerListComponent implements OnInit, OnDestroy {
  manufacturers: Manufacturer[];
  subscription: Subscription;

  constructor(private manufacturerService: ManufacturerService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscription = this.manufacturerService.manufacturersChanged.subscribe(
      (manufacturer: Manufacturer[]) => {
        this.manufacturers = manufacturer;
      }
    );

    this.manufacturers = this.manufacturerService.getManufacturers();
  }

  onNewManufacturer() {
    this.router.navigate(['new'], {relativeTo: this.route});
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
