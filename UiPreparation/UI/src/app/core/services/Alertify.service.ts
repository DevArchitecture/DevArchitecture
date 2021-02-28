import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {



  constructor(private httpClient: HttpClient, public translateService: TranslateService) {

    alertify.set('notifier', 'position', 'top-right');
  }

  success(message: string) {

    this.translateService.get(message).subscribe((mes: string) => {
      alertify.success(mes);
    });
    
  }

  error(message: string) {
    this.translateService.get(message).subscribe((mes: string) => {      
      alertify.error(mes);
    });

    
  }

  info(message: string) {

    this.translateService.get(message).subscribe((mes: string) => {
      alertify.info(mes);
    });

  }

  warning(message: string) {

    this.translateService.get(message).subscribe((mes: string) => {
      alertify.warning(message);
    });
 
  }

  confirmDelete(url: string, values: any) {


    // alertify.confirm("aa", "bb", () => { this.deleteRequest(url,values); }
    // , () => { this.error("hata")});

    //alertify.confirm('Confirm Message', function(){ alertify.success('Ok') }, function(){ alertify.error('Cancel')});
    // 
    // alertify.confirm('Delete Message',this.deleteRequest(url,values),this.error("Hata"));

    // 



    alertify.alert()
      .setting({
        'label': 'Agree',
        'message': 'This dialog is ',
        'onok': this.delete(url, values)
      }).show();


  }

  delete(url: string, values: any) {
    
    this.httpClient.request("delete", url, values)
  }




}
