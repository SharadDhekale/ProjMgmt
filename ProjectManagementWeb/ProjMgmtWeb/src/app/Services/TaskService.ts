import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProjTask } from '../Model/ProjTask';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjTaskService {

  apiBaseUrl: string = "http://localhost/ProjectManagementAPI/api/Tasks/";

  constructor(private _http: HttpClient) { }

  GetTasksList(): Observable<any> {
    return this._http.get(this.apiBaseUrl + "GetTasksList").pipe(map(res => res));
  }
  GetTask(Id: number): Observable<any> {
    return this._http.get(this.apiBaseUrl + "GetTask/?id="+name).pipe(map(res => res));
  }
//GetTaskByProjectId
GetTaskByProjectId(Id: number): Observable<any> {
  return this._http.get(this.apiBaseUrl + "GetTaskByProjectId/?id="+Id).pipe(map(res => res));
}
  AddTask(obj: ProjTask): Observable<any> {
    return this._http.post(this.apiBaseUrl + "AddTask", obj).pipe(map(res => res));
  }
  UpdateTask(obj: ProjTask): Observable<any> {
    return this._http.post(this.apiBaseUrl + "UpdateTask", obj).pipe(map(res => res));
  }
  DeleteTask(obj: ProjTask): Observable<any> {
    return this._http.post(this.apiBaseUrl + "DeleteTask", obj).pipe(map(res => res));
  }

}