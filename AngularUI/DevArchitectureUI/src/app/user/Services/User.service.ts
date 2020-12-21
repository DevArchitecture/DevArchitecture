import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LookUp } from 'app/GlobalServices/Models/LookUp';
import { Config } from 'app/StaticFiles/Config';
import { Observable } from 'rxjs';
import { User } from '../models/User';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  getUserList(): Observable<User[]> {

    return this.httpClient.get<User[]>(Config.GetApiUrl + "/users/getall");

  }

  getUserById(id: number): Observable<User> {

    return this.httpClient.get<User>(Config.GetApiUrl + "/users/getbyid?userId=" + id);
  }


  addUser(user: User): Observable<any> {

    var result = this.httpClient.post(Config.GetApiUrl + "/users/", user, { responseType: 'text' });
    return result;
  }

  updateUser(user:User):Observable<any> {

    var result = this.httpClient.put(Config.GetApiUrl + "/users/", user, { responseType: 'text' });
    return result;
  }

  deleteUser(id: number) {
    return this.httpClient.request('delete', Config.GetApiUrl + "/users/", { body: {userId:id} });
  }

  getUserGroupPermissions(userId:number):Observable<LookUp[]>{
    return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/UserGroups/getusergroupbyuserid?id=" + userId);
  }

  getUserClaims(userId:number){
     return this.httpClient.get<LookUp[]>(Config.GetApiUrl + "/UserClaims/getoperationclaimbyuserid?id=" + userId);
  }

  saveUserClaims(userId:number,claims:number[] ):Observable<any> {

    var result = this.httpClient.put(Config.GetApiUrl + "/UserClaims/", {UserId:userId, ClaimId:claims }, { responseType: 'text' });
    return result;

  }

  saveUserGroupPermissions(userId:number, groups:number[]):Observable<any> {
    var result = this.httpClient.put(Config.GetApiUrl + "/UserGroups/", {UserId:userId, GroupId:groups }, { responseType: 'text' });
    return result;

  }

}
