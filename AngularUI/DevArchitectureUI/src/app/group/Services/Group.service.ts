import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LookUp } from 'app/GlobalServices/Models/LookUp';
import { Config } from 'app/StaticFiles/Config';
import { Observable } from 'rxjs';
import { Group } from '../Models/Group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private httpClient: HttpClient) { }

  getGroupList(): Observable<Group[]> {

    return this.httpClient.get<Group[]>(Config.GetApiUrl + "/Groups/getall")
  }

  getGroupById(id: number): Observable<Group> {
    return this.httpClient.get<Group>(Config.GetApiUrl + "/Groups/getbyid?groupId=" + id);
  }

  getGroupClaims(id: number): Observable<LookUp[]> {
    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/GroupClaims/getgroupclaimsbygroupid?id=" + id); 
  }

  getGroupUsers(id:number):Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/UserGroups/getusersingroupbygroupid?id=" + id);  
  }

  addGroup(group:Group):Observable<any>{

    var result = this.httpClient.post(Config.GetApiUrl + "/groups/", group, { responseType: 'text' });
    return result;
  }

  updateGroup(group:Group):Observable<any> {
    var result = this.httpClient.put(Config.GetApiUrl + "/groups/", group, { responseType: 'text' });
    return result; 
  }

  deleteGroup(id:number){
    return this.httpClient.request('delete', Config.GetApiUrl + "/groups/", { body: {Id:id} });
  }

  saveGroupClaims(groupId:number,claims:number[] ):Observable<any> {
    var result = this.httpClient.put(Config.GetApiUrl + "/GroupClaims/", {GroupId:groupId, ClaimIds:claims }, { responseType: 'text' });
    return result;
  }

  saveGroupUsers(groupId:number,userIds:number[] ):Observable<any> {
    var result = this.httpClient.put(Config.GetApiUrl + "/UserGroups/updatebygroupid", {GroupId:groupId, UserIds:userIds }, { responseType: 'text' });
    return result;
  }


}
