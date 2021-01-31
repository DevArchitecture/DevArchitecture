import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from 'environments/environment';
import { Observable } from 'rxjs';
import { Translate } from '../Models/Translate';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {

  constructor(private httpClient: HttpClient) { }


  getTranslateList(): Observable<Translate[]> {

    return this.httpClient.get<Translate[]>(environment.getApiUrl + '/translates/gettranslatelistdto')
  }

  getTranslate(id: number): Observable<Translate> {
    return this.httpClient.get<Translate>(environment.getApiUrl + '/translates/getbyid?translateId='+id)
  }

  addTranslate(translate: Translate): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/translates/', translate, { responseType: 'text' });
  }

  updateTranslate(translate: Translate): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/translates/', translate, { responseType: 'text' });

  }

  deleteTranslate(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/translates/', { body: { Id: id } });
  }


}