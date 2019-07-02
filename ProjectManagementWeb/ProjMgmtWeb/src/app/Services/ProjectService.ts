import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Project } from '../Model/Project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  apiBaseUrl: string = "http://localhost:2385/api/Project/";

  constructor(private _http: HttpClient) { }

  GetProjectList(): Observable<any> {
    return this._http.get(this.apiBaseUrl + "GetProjectList").pipe(map(res => res));
  }

  GetProject(name: string): Observable<any> {
    return this._http.get(this.apiBaseUrl + "GetProject/"+name).pipe(map(res => res));
  }

  AddProject(obj: Project): Observable<any> {
    return this._http.post(this.apiBaseUrl + "AddProject", obj).pipe(map(res => res));
  }
  UpdateProject(obj: Project): Observable<any> {
    return this._http.post(this.apiBaseUrl + "UpdateProject", obj).pipe(map(res => res));
  }
  DeleteProject(obj: Project): Observable<any> {
    return this._http.post(this.apiBaseUrl + "DeleteProject", obj).pipe(map(res => res));
  }

}