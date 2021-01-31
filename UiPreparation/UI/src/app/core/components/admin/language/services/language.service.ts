import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from  'environments/environment'
import { Observable } from 'rxjs';
import { Language } from '../Models/Language';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private httpClient: HttpClient) { }


  getLanguageList(): Observable<Language[]> {

    return this.httpClient.get<Language[]>(environment.getApiUrl + '/languages/getall')
  }

  getLanguage(id: number): Observable<Language> {
    return this.httpClient.get<Language>(environment.getApiUrl  + '/languages/getbyid?languageId='+id)
  }

  addLanguage(language: Language): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/languages/', language, { responseType: 'text' });
  }

  updateLanguage(language: Language): Observable<any> {
    return this.httpClient.put(environment.getApiUrl  + '/languages/', language, { responseType: 'text' });

  }

  deleteLanguage(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl  + '/languages/', { body: { Id: id } });
  }


}