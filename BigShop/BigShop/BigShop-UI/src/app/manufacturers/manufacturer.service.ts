import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Manufacturer } from "./manufacturer.model";

@Injectable({providedIn: 'root'})
export class ManufacturerService {
    manufacturersChanged = new Subject<Manufacturer[]>();

    private manufacturers: Manufacturer[] = [
        // new Manufacturer('Samsung', 0),
        // new Manufacturer('Apple', 1)
    ];
    
    constructor(private http: HttpClient){}
    
    setManufacturers(manufacturers: Manufacturer[]) {
          this.manufacturers = manufacturers;
          this.manufacturersChanged.next(this.manufacturers.slice());
    }

    loadManufacturers() {
        this.http.get<Manufacturer[]>('http://localhost:5000/api/manufacturers').subscribe(
            manufacturers => {
                this.setManufacturers(manufacturers);
            }
        );
    }

    getManufacturers() {
        this.loadManufacturers();
        return this.manufacturers.slice();
    }

    getManufacturer(id: number) {
        return this.manufacturers.find(m => m.id == id);
    }

    addManufacturer(manufacturer: Manufacturer) {
        this.http.post<Manufacturer>('http://localhost:5000/api/manufacturers', manufacturer).subscribe(
            manufacturer => {
                this.manufacturers.push(manufacturer);
                this.loadManufacturers();
            }
        );
    }

    updateManufacturer(id: number, newManufacturer: Manufacturer) {
        let index = this.manufacturers.indexOf(this.manufacturers.find(c => c.id == id))
        this.manufacturers[index] = newManufacturer;

        const updateUrl = 'http://localhost:5000/api/manufacturers/' + id;
        this.http.put(updateUrl, newManufacturer).subscribe(
            response => {
                this.loadManufacturers();
            }
        );
    }

    deleteManufacturer(id: number) {
        const deleteUrl = 'http://localhost:5000/api/manufacturers/' + id;
        this.http.delete(deleteUrl).subscribe(
            response => {
                this.loadManufacturers();
            }, error => {
                alert('Cannot delete manufacturer with products in it');
                // this.errorEvent.next(error.message);
            }
        );
    }
}