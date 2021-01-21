import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  

  constructor(private httpClient: HttpClient,public translateService:TranslateService) {
    
    alertify.set('notifier', 'position', 'top-right');
  }

  success(message: string) {
    alertify.success(this.convertMessage(message));
  }

  error(message: string) {
    alertify.error(this.convertMessage(message));
  }

  info(message: string) {
    alertify.info(this.convertMessage(message));
  }

  warning(message: string) {
    alertify.warning(this.convertMessage(message));
  }

  convertMessage(messagekey:string)
  {
    this.translateService.get(messagekey);
  }

  confirmDelete(url: string, values: any) {


    // alertify.confirm("aa", "bb", () => { this.deleteRequest(url,values); }
    // , () => { this.error("hata")});

    //alertify.confirm('Confirm Message', function(){ alertify.success('Ok') }, function(){ alertify.error('Cancel')});
    // debugger;
    // alertify.confirm('Delete Message',this.deleteRequest(url,values),this.error("Hata"));

    // 



    alertify.alert()
      .setting({
        'label': 'Agree',
        'message': 'This dialog is ',
        'onok': this.delete(url,values)
      }).show();


  }

  delete(url: string, values: any)
  {
    debugger;
    this.httpClient.request("delete", url, values)
  }




}
