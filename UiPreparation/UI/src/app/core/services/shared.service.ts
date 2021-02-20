import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs/Rx';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private subject = new Subject<any>();

  sendChangeUserNameEvent(){
    this.subject.next();

  }
  getChangeUserNameClickEvent():Observable<any>{
    return this.subject.asObservable();
 }

}
