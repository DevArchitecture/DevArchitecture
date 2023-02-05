import { Injectable } from '@angular/core';
import { ApiUrl } from 'app/core/constants/api-url';
import { HttpEntityRepositoryService } from 'app/core/services/http-entity-repository.service';
import { Observable } from 'rxjs';
import { Language } from '../Models/Language';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private service: HttpEntityRepositoryService<any>,
    private readonly _httpClient: HttpClient) { }

  getLanguageList(): Observable<Language[]> {
    return this._httpClient.get<Language[]>(environment.getApiUrl +'/languages/');
  }

  getLanguage(id: number): Observable<Language> {
    return this._httpClient.get<Language>(environment.getApiUrl +`/languages/${id}`);
  }

  addLanguage(language: Language): Observable<any> {
    return this._httpClient.post(environment.getApiUrl +"/languages/", language, { responseType: 'text' });
  }

  updateLanguage(language: Language): Observable<any> {
    return this._httpClient.put(environment.getApiUrl +"/languages/", language, { responseType: 'text' });
  }

  deleteLanguage(id: number) {
    return this._httpClient.delete(environment.getApiUrl +`/languages/${id}`);
  }
}