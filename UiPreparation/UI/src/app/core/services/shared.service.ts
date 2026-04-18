import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private subject = new Subject<void>();

  sendChangeUserNameEvent(){
    this.subject.next();

  }
  getChangeUserNameClickEvent():Observable<void>{
    return this.subject.asObservable();
 }

}
