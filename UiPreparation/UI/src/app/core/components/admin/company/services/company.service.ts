import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Company } from '../models/Company';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private readonly _httpClient: HttpClient) { }


  getCompanyList(): Observable<Company[]> {

    return this._httpClient.get<Company[]>(environment.getApiUrl + '/companies/')
  }

  getCompanyById(id: number): Observable<Company> {
    return this._httpClient.get<Company>(environment.getApiUrl + `/companies/${id}`)
  }

  addCompany(company: Company): Observable<any> {

    return this._httpClient.post(environment.getApiUrl + '/companies/', company, { responseType: 'text' });
  }

  updateCompany(company: Company): Observable<any> {
    return this._httpClient.put(environment.getApiUrl + '/companies/', company, { responseType: 'text' });

  }

  deleteCompany(id: number) {
    return this._httpClient.request('delete', environment.getApiUrl + `/companys/${id}`, { body: { id: id } });
  }


}