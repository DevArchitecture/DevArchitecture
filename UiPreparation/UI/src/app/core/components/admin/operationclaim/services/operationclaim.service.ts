import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from "environments/environment"
import { Observable } from 'rxjs';
import { OperationClaim } from '../Models/OperationClaim';

@Injectable({
  providedIn: 'root'
})
export class OperationClaimService {

  constructor(private httpClient: HttpClient) { }


  getOperationClaimList(): Observable<OperationClaim[]> {

    return this.httpClient.get<OperationClaim[]>(environment.getApiUrl + '/operationClaims/getall')
  }

  getOperationClaim(id: number): Observable<OperationClaim> {
    return this.httpClient.get<OperationClaim>(environment.getApiUrl  + '/operationClaims/getbyid?id='+id)
  }

  updateOperationClaim(operationClaim: OperationClaim): Observable<any> {
    return this.httpClient.put(environment.getApiUrl  + '/operationClaims/', operationClaim, { responseType: 'text' });

  }


}