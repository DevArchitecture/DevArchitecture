import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Group } from '../models/group';
import { environment } from '../../../../../../environments/environment';
import { LookUp } from 'app/core/models/lookUp';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private httpClient: HttpClient) { }

  getGroupList(): Observable<Group[]> {

    return this.httpClient.get<Group[]>(environment.getApiUrl + "/Groups/getall")
  }

  getGroupById(id: number): Observable<Group> {
    return this.httpClient.get<Group>(environment.getApiUrl + "/Groups/getbyid?groupId=" + id);
  }

  getGroupClaims(id: number): Observable<LookUp[]> {
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/GroupClaims/getgroupclaimsbygroupid?id=" + id); 
  }

  getGroupUsers(id:number):Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/UserGroups/getusersingroupbygroupid?id=" + id);  
  }

  addGroup(group:Group):Observable<any>{

    var result = this.httpClient.post(environment.getApiUrl + "/groups/", group, { responseType: 'text' });
    return result;
  }

  updateGroup(group:Group):Observable<any> {
    var result = this.httpClient.put(environment.getApiUrl + "/groups/", group, { responseType: 'text' });
    return result; 
  }

  deleteGroup(id:number){
    return this.httpClient.request('delete', environment.getApiUrl + "/groups/", { body: {Id:id} });
  }

  saveGroupClaims(groupId:number,claims:number[] ):Observable<any> {
    var result = this.httpClient.put(environment.getApiUrl + "/GroupClaims/", {GroupId:groupId, ClaimIds:claims }, { responseType: 'text' });
    return result;
  }

  saveGroupUsers(groupId:number,userIds:number[] ):Observable<any> {
    var result = this.httpClient.put(environment.getApiUrl + "/UserGroups/updatebygroupid", {GroupId:groupId, UserIds:userIds }, { responseType: 'text' });
    return result;
  }


}
