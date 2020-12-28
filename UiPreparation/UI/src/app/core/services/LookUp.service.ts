import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LookUp } from '../models/LookUp';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LookUpService {

  constructor(private httpClient: HttpClient) { }

  getGroupLookUp(): Observable<LookUp[]> {

    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/Groups/getgrouplookup")
  }

  getOperationClaimLookUp(): Observable<LookUp[]> {

    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/OperationClaims/getoperationclaimlookup")
  }

  getUserLookUp():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/Users/getuserlookup")
  }

  getCategoryLookUp():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/Categories/getcategorylookupquery")
  }

  getLanguageLookup():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/Languages/getlookup")
  }

}
