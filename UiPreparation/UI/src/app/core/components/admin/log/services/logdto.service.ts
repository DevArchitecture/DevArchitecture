import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LogDto } from '../models/LogDto';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LogDtoService {

  constructor(private httpClient: HttpClient) { }


  getLogDtoList(): Observable<LogDto[]> {

    return this.httpClient.get<LogDto[]>(environment.getApiUrl + '/logs/getall')
  }

}