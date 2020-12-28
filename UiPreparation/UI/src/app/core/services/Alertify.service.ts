import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor(private httpClient: HttpClient) {
    alertify.set('notifier', 'position', 'top-right');
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  info(message: string) {
    alertify.info(message);
  }

  warning(message: string) {
    alertify.warning(message);
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
