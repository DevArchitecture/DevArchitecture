import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../Models/Product';
import { ProductListDto } from '../Models/ProductListDto';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { }


  getProductList(): Observable<ProductListDto[]> {

    return this.httpClient.get<ProductListDto[]>(environment.getApiUrl + '/products/getproductdtolist')
  }

  getProduct(id: number): Observable<Product> {
    return this.httpClient.get<Product>(environment.getApiUrl+ '/products/getbyid?productId='+id)
  }

  addProduct(product: Product): Observable<any> {

    return this.httpClient.post(environment.getApiUrl+ '/products/', product, { responseType: 'text' });
  }

  updateProduct(product: Product): Observable<any> {
    return this.httpClient.put(environment.getApiUrl+ '/products/', product, { responseType: 'text' });

  }

  deleteProduct(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl+ '/products/', { body: { Id: id } });
  }


}
