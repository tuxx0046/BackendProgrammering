import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Product } from "./product.model";

@Injectable({providedIn: 'root'})
export class ProductService {
    productsChanged = new Subject<Product[]>();

    private products: Product[] = [
        // new Product('Samsung TV', 1500.55, 'adfghjkltreds', 2500.66, 0, 0, 0),
        // new Product('Apple ipad', 5500.55,'edfyujkmnbgfc', 200.66, 1, 1, 1),    
      ];

    constructor(private http: HttpClient) {}

    setProducts(products: Product[]) {
      this.products = products;
      this.productsChanged.next(this.products.slice());
    }

    loadProducts() {
      this.http.get<Product[]>('http://localhost:5000/api/products').subscribe(
        products => {
          this.setProducts(products);
        }
      );
    }

    getProducts() {
        this.loadProducts();
        return this.products.slice();
    }

    getProduct(id: number) {
        return this.products.find(p => p.id == id);
    }

    addProduct(product: Product) {
        this.http.post<Product>('http://localhost:5000/api/products', product).subscribe(
          product => {
            this.products.push(product);
            this.loadProducts();
          }
        );
    }

    updateProduct(id: number, newProduct: Product) {
      let index = this.products.indexOf(this.products.find(c => c.id == id))
      this.products[index] = newProduct;

        const updateUrl = 'http://localhost:5000/api/products/' + id;
        this.http.put(updateUrl, newProduct).subscribe(
          response => {
            this.loadProducts();
          }
        );
    }

    deleteProduct(id: number) {
        const deleteUrl = 'http://localhost:5000/api/products/' + id;
        this.http.delete(deleteUrl).subscribe(
          response => {
            this.loadProducts();
          }
        );
  }
}
