import { Subject } from "rxjs";
import { Product } from "./product.model";

export class ProductService {

    private products: Product[] = [
        new Product(
          'Samsung TV',
          1500.55,
          'adfghjkltreds',
          2500.66,
          0,
          0,
          0
        ),
        new Product(
          'Apple ipad',
          5500.55,
          'edfyujkmnbgfc',
          200.66,
          1,
          1,
          1
        ),
    
      ];

    getProducts() {
        return this.products.slice();
    }

    getProduct(id: number) {
      return this.products[id];
    }
}