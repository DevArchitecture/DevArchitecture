import { Injectable } from '@angular/core';
import { ApiUrl } from 'app/core/constants/api-url';
import { HttpEntityRepositoryService } from 'app/core/services/http-entity-repository.service';
import { Observable } from 'rxjs';
import { Language } from '../Models/Language';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private service: HttpEntityRepositoryService<any>,
    private readonly _httpClient: HttpClient) { }

  getLanguageList(): Observable<Language[]> {
    return this._httpClient.getAll('/languages/');
  }

  getLanguage(id: number): Observable<Language> {
    return this._httpClient.get(`/languages/${id}`);
  }

  addLanguage(language: Language): Observable<any> {
    return this._httpClient.add('/languages/', language);
  }

  updateLanguage(language: Language): Observable<any> {
    return this._httpClient.update(`/languages/${language.id}`, language);
  }

  deleteLanguage(id: number) {
    return this._httpClient.delete(`/languages/${id}`);
  }
}