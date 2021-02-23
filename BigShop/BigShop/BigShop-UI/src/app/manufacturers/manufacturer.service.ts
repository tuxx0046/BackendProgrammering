import { Subject } from "rxjs";
import { Manufacturer } from "./manufacturer.model";

export class ManufacturerService {
    manufacturersChanged = new Subject<Manufacturer[]>();

    private manufacturers: Manufacturer[] = [
        new Manufacturer('Samsung', 0),
        new Manufacturer('Apple', 1)
      ];
    
    getManufacturers() {
        return this.manufacturers.slice();
    }

    getManufacturer(id: number) {
        return this.manufacturers.find(m => m.id == id);
    }

    addManufacturer(manufacturer: Manufacturer) {
        let nextIndex = this.manufacturers.length;
        manufacturer.id = nextIndex;
        this.manufacturers.push(manufacturer);

        this.manufacturersChanged.next(this.manufacturers.slice());
    }

    updateManufacturer(id: number, newManufacturer: Manufacturer) {
        let index = this.manufacturers.indexOf(this.manufacturers.find(m => m.id == id))
        this.manufacturers[index] = newManufacturer;

        this.manufacturersChanged.next(this.manufacturers.slice());
    }

    deleteManufacturer(id: number) {
        let manufacturer = this.manufacturers.find(m => m.id == id)
        let index = this.manufacturers.indexOf(manufacturer);
        this.manufacturers.splice(index, 1);

        this.manufacturersChanged.next(this.manufacturers.slice());
    }
}