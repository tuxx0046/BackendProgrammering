import { Subject } from "rxjs";
import { Manufacturer } from "./manufacturer.model";

export class ManufacturerService {

    private manufacturers: Manufacturer[] = [
        new Manufacturer('Samsung', 0),
        new Manufacturer('Apple', 1)
      ];
    
    getManufacturers() {
        return this.manufacturers.slice();
    }

    getManufacturer(id: number) {
        return this.manufacturers[id];
    }
}