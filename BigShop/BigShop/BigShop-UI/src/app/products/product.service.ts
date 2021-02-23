import { Subject } from "rxjs";
import { Product } from "./product.model";

export class ProductService {
    productsChanged = new Subject<Product[]>();

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
      return this.products.find(p => p.id == id);
    }

    addProduct(product: Product) {
        let nextIndex = this.products.length;
        product.id = nextIndex;
        this.products.push(product);

        this.productsChanged.next(this.products.slice());
    }

    updateProduct(id: number, newProduct: Product) {
      let index = this.products.indexOf(this.products.find(c => c.id == id))
      this.products[index] = newProduct;

      this.productsChanged.next(this.products.slice());
    }

    deleteProduct(id: number) {
      let product = this.products.find(p => p.id == id)
      let index = this.products.indexOf(product);
      this.products.splice(index, 1);

      this.productsChanged.next(this.products.slice());
  }
}
