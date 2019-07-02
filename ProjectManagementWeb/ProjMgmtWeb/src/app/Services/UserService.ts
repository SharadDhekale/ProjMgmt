import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../Model/User';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiBaseUrl: string = "http://localhost:2385/api/Users/";

  constructor(private _http: HttpClient) { }

  GetUserList(): Observable<any> {
    return this._http.get(this.apiBaseUrl + "GetUserList").pipe(map(res => res));
  }
  GetUser(name: string): Observable<any> {
    return this._http.get(this.apiBaseUrl + "GetUser/?name="+name).pipe(map(res => res));
  }

  AddUser(obj: User): Observable<any> {
    return this._http.post(this.apiBaseUrl + "Adduser", obj).pipe(map(res => res));
  }
  UpdateUser(id:number,obj: User): Observable<any> {
   
    return this._http.put(this.apiBaseUrl + "UpdateUser?id="+ id,obj).pipe(map(res => res));
  }
  DeleteUser(obj: User): Observable<any> {
    return this._http.post(this.apiBaseUrl + "DeleteUser", obj).pipe(map(res => res));
  }

}