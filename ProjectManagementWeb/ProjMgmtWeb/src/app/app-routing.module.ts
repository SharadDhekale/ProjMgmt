import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent } from './project/project.component';
import { AddUserComponent } from './add-user/add-user.component';
import {RouterModule,Routes} from "@angular/router";
import {ViewTasksComponent} from './view-tasks/view-tasks.component';
import {AddTasksComponent} from './add-tasks/add-tasks.component';
import { from } from 'rxjs';

const APP_ROUTES: Routes = [
  { path: '', component: ProjectComponent },
  { path: 'addtask', component: AddTasksComponent },
  { path: 'adduser', component: AddUserComponent },
  { path: 'viewtask', component: ViewTasksComponent },
  { path: 'updatetask/:id', component: ProjectComponent },
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(APP_ROUTES)
  ],
  exports:[RouterModule],
  declarations: []
})


export class AppRoutingModule { }
