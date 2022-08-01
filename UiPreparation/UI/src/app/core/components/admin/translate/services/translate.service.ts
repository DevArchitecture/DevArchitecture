import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from 'environments/environment';
import { Observable } from 'rxjs';
import { Translate } from '../Models/Translate';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {

  constructor(private readonly _httpClient: HttpClient) { }


  getTranslateList(): Observable<Translate[]> {

    return this._httpClient.get<Translate[]>(environment.getApiUrl + '/translates/dtos')
  }

  getTranslate(id: number): Observable<Translate> {
    return this._httpClient.get<Translate>(environment.getApiUrl + `/translates/${id}`)
  }

  addTranslate(translate: Translate): Observable<any> {

    return this._httpClient.post(environment.getApiUrl + '/translates/', translate, { responseType: 'text' });
  }

  updateTranslate(translate: Translate): Observable<any> {
    return this._httpClient.put(environment.getApiUrl + '/translates/', translate, { responseType: 'text' });

  }

  deleteTranslate(id: number) {
    return this._httpClient.request('delete', environment.getApiUrl + `/translates/${id}`);
  }
}