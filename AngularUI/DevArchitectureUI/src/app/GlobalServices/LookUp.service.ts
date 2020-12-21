import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Config } from 'app/StaticFiles/Config';
import { Observable } from 'rxjs';
import { LookUp } from './Models/LookUp';

@Injectable({
  providedIn: 'root'
})
export class LookUpService {

  constructor(private httpClient: HttpClient) { }

  getGroupLookUp(): Observable<LookUp[]> {

    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/Groups/getgrouplookup")
  }

  getOperationClaimLookUp(): Observable<LookUp[]> {

    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/OperationClaims/getoperationclaimlookup")
  }

  getUserLookUp():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/Users/getuserlookup")
  }

  getCategoryLookUp():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/Categories/getcategorylookupquery")
  }

}
