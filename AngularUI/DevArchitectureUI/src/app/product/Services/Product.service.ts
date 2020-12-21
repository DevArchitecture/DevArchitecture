import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Config } from 'app/StaticFiles/Config';
import { Observable } from 'rxjs';
import { Product } from '../Models/Product';
import { ProductListDto } from '../Models/ProductListDto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { }


  getProductList(): Observable<ProductListDto[]> {

    return this.httpClient.get<ProductListDto[]>(Config.GetApiUrl + '/products/getproductdtolist')
  }

  getProduct(id: number): Observable<Product> {
    return this.httpClient.get<Product>(Config.GetApiUrl + '/products/getbyid?productId='+id)
  }

  addProduct(product: Product): Observable<any> {

    return this.httpClient.post(Config.GetApiUrl + '/products/', product, { responseType: 'text' });
  }

  updateProduct(product: Product): Observable<any> {
    return this.httpClient.put(Config.GetApiUrl + '/products/', product, { responseType: 'text' });

  }

  deleteProduct(id: number) {
    return this.httpClient.request('delete', Config.GetApiUrl + '/products/', { body: { Id: id } });
  }


}
