import { Injectable } from '@angular/core';
import {HttpInterceptor,HttpRequest,HttpHandler,HttpEvent, HttpProgressEvent, HttpResponse, HttpUserEvent, HttpSentEvent, HttpHeaderResponse} from '@angular/common/http';
import { Observable} from 'rxjs';
import { HtmlAstPath } from '@angular/compiler';
 @Injectable()
export class HttpHeaderInterceptor implements HttpInterceptor {

  intercept(req:HttpRequest<any>, next:HttpHandler)
  :Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> |HttpUserEvent<any>>
  {
    let constnewReq :HttpRequest<any>=null;

    constnewReq= req.clone(
        {
          headers: req.headers.set('Content-Type','application/json')
          .append('Access-Control-Allow-Headers', 'Content-Type')
          .append('Access-Control-Allow-Methods', 'GET')
          .append('Access-Control-Allow-Origin', '*')
        });
    return next.handle(constnewReq);
  }
  constructor() { }
}
