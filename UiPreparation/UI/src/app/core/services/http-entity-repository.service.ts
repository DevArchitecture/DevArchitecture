import { Injectable } from "@angular/core";
import {  HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "environments/environment";

//If the token is sent with interception, there is no need to send header information here, 
//but if the token information does not go in the request header with interception, it must be sent with a method in this class.

@Injectable()
export class HttpEntityRepositoryService<T> {
  
  constructor(private httpClient: HttpClient) { }

  /// /api/[controller] - GET
  getAll(_url: string): Observable<T[]> {
    return this.httpClient.get<T[]>( 
      environment.getApiUrl + _url);
  }
 
  //api/[controller]/:id - GET
  get(_url: string, id?: number): Observable<T> {
    return this.httpClient.get<T>(
      environment.getApiUrl +  _url + ((id != undefined && id != null) ? + id : ""),
    );
  }

  /// /api/[controller] - POST
  add(_url: string, _content: any): Observable<T> {
    return this.httpClient.post<T>(
      environment.getApiUrl +  _url,
      _content,
    );
  }

  // /api/[controller] - PUT
  update(_url: string, _content: any): Observable<T> {
    return this.httpClient.put<T>(
      environment.getApiUrl + _url,
      _content,
    );
  }

  // /api/[controller]/:id - DELETE
  delete(_url: string, id: number): Observable<T> {
    return this.httpClient.delete<T>(
      environment.getApiUrl + _url  + id,
    );
  }
}
