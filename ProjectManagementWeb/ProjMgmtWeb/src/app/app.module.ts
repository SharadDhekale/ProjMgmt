import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule,ReactiveFormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ProjectComponent } from './project/project.component';
import { AddUserComponent } from './add-user/add-user.component';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { SortPipe } from './PipeExtension/StringSort.pipe'//'./Pipe/sort.pipe';
import { DateSortPipe } from './PipeExtension/DateSortPipe';
import {DatePipe} from '@angular/common';
import {HttpHeaderInterceptor} from './Services/interceptor-service.service';
import { AddTasksComponent } from './add-tasks/add-tasks.component';
import { ViewTasksComponent } from './view-tasks/view-tasks.component';
@NgModule({
  declarations: [
    AppComponent,
    ProjectComponent,
    AddUserComponent,
    AddTasksComponent,
    ViewTasksComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [DatePipe,SortPipe,DateSortPipe,
  //{provide: HTTP_INTERCEPTORS, useClass:HttpHeaderInterceptor,multi:true}
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
