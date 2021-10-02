import { Injectable } from '@angular/core';
import { ApiUrl } from 'app/core/constants/api-url';
import { HttpEntityRepositoryService } from 'app/core/services/http-entity-repository.service';
import { Observable } from 'rxjs';
import { Language } from '../Models/Language';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private service: HttpEntityRepositoryService<any>) { }

  getLanguageList(): Observable<Language[]> {
    return this.service.getAll(ApiUrl.GETALL_LANGUAGES);
  }

  getLanguage(id: number): Observable<Language> {
    return this.service.get(ApiUrl.GET_LANGUAGES,id);
  }

  addLanguage(language: Language): Observable<any> {
    return this.service.add(ApiUrl.LANGUAGES, language);
  }

  updateLanguage(language: Language): Observable<any> {
    return this.service.update(ApiUrl.LANGUAGES, language);
  }

  deleteLanguage(id: number) {
    return this.service.delete(ApiUrl.LANGUAGES,id);
  }
}