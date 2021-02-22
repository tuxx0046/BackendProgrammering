import { EventEmitter } from "@angular/core";
import { Manufacturer } from "./manufacturer.model";

export class ManufacturerService {
    manufacturerSelected = new EventEmitter<Manufacturer>();

    private manufacturers: Manufacturer[] = [
        new Manufacturer('Samsung', 0),
        new Manufacturer('Apple', 1)
      ];
    
    getManufacturers() {
        return this.manufacturers.slice();
    }
}