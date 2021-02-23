import { Warehouse } from "./warehouse.model";

export class WarehouseService {
    private warehouses: Warehouse[] = [
        new Warehouse(0, 'Odense varehus', 'Munkebjergvej 130', 0),
        new Warehouse(1, 'Århus varehus', 'Centrumpladsen 22', 1)    
      ];

    getWarehouses() {
        return this.warehouses.slice();
    }

    getWarehouse(id: number) {
      return this.warehouses[id];
    }

    
}