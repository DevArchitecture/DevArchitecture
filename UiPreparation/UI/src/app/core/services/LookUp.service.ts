import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { LookUp } from '../models/lookUp';


@Injectable({
  providedIn: 'root'
})
export class LookUpService {

  constructor(private httpClient: HttpClient) { }

  getGroupLookUp(): Observable<LookUp[]> {

    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/groups/lookups")
  }

  getOperationClaimLookUp(): Observable<LookUp[]> {

    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/operation-claims/lookups")
  }

  getUserLookUp():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/users/lookups")
  }

  getLanguageLookup():Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/languages/lookups")
  }

}
