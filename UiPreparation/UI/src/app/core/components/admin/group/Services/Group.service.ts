import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Group } from "../models/group";
import { environment } from "../../../../../../environments/environment";
import { LookUp } from "app/core/models/lookUp";

@Injectable({
  providedIn: "root",
})
export class GroupService {
  constructor(private readonly _httpClient: HttpClient) {}

  getGroupList(): Observable<Group[]> {
    return this._httpClient.get<Group[]>(environment.getApiUrl + "/groups/");
  }

  getGroupById(id: number): Observable<Group> {
    return this._httpClient.get<Group>(environment.getApiUrl + `/groups/${id}`);
  }

  getGroupClaims(id: number): Observable<LookUp[]> {
    return this._httpClient.get<LookUp[]>(
      environment.getApiUrl + `/group-claims/groups/${id}`
    );
  }

  getGroupUsers(id: number): Observable<LookUp[]> {
    return this._httpClient.get<LookUp[]>(
      environment.getApiUrl + `/user-groups/groups/${id}/users`
    );
  }

  addGroup(group: Group): Observable<any> {
    var result = this._httpClient.post(
      environment.getApiUrl + "/groups/",
      group,
      { responseType: "text" }
    );
    return result;
  }

  updateGroup(group: Group): Observable<any> {
    var result = this._httpClient.put(
      environment.getApiUrl + "/groups/",
      group,
      { responseType: "text" }
    );
    return result;
  }

  deleteGroup(id: number) {
    return this._httpClient.request(
      "delete",
      environment.getApiUrl + `/groups/${id}`
    );
  }

  saveGroupClaims(groupId: number, claims: number[]): Observable<any> {
    var result = this._httpClient.put(
      environment.getApiUrl + `/group-claims/`,
      { GroupId: groupId, ClaimIds: claims },
      { responseType: "text" }
    );
    return result;
  }

  saveGroupUsers(groupId: number, userIds: number[]): Observable<any> {
    var result = this._httpClient.put(
      environment.getApiUrl + "/user-groups/groups/",
      { GroupId: groupId, UserIds: userIds },
      { responseType: "text" }
    );
    return result;
  }
}