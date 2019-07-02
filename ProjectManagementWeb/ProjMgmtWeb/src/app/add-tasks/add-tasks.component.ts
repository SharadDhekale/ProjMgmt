import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Project } from '../Model/Project';
import { ProjTaskService } from '../Services/TaskService';
import { ProjectService } from '../Services/ProjectService';
import { UserService } from '../Services/UserService';
import { ProjTask } from '../Model/ProjTask';
import { User } from '../Model/User';
import { Router, ActivatedRoute } from '@angular/router';
import { fromEvent, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-add-tasks',
  templateUrl: './add-tasks.component.html',
  styleUrls: ['./add-tasks.component.css']
})
export class AddTasksComponent implements OnInit, AfterViewInit {
  taskObj: ProjTask;
  lstItem: ModelView[] = [];
  searchTitle: string;
  selectedproject: Project;
  selectedparenttask: ProjTask;
  selecteduser: User;
  projectName: string;
  parentTaskName: string;
  userName: string;
  searchId: number;
  taskName: string;
  isPrntTskChckd: boolean;
  dateIsValid: boolean = true;
  startDt: Date;
  endDt: Date;
  lstProjects: Project[];
  lstUsers: User[];
  lstParentTask: ProjTask[];
  mv: ModelView;
  responseMsg: string;
  taskId: number;
  buttonText: string = "";
  isUpdate: boolean;
  //isParentTsk:boolean;
  constructor(private _datePipe: DatePipe, 
              private _service: ProjTaskService, 
              private _projService: ProjectService,
              private _userService:UserService,
              private router: Router, 
              private route: ActivatedRoute) {
    this.taskObj = new ProjTask();
    this.responseMsg = "";
    this.isUpdate = false;
    this.route.params.subscribe(p => {
      this.taskId = p["id"];
      // this.isParentTsk = p["pt"];
    });
    if (this.taskId === undefined) {
      this.isPrntTskChckd = false;
      this.dateIsValid = true;
      this.taskObj.StartDate = new Date();
      this.taskObj.EndDate = new Date();
      this.taskObj.EndDate.setDate(this.taskObj.StartDate.getDate() + 1);
      this.buttonText = "Add Task";
    }
    else {
      this.buttonText = "Update Task";
      this.isUpdate = true;

      //if (!this.isParentTsk) {
      this._service.GetTask(this.taskId).subscribe(res => {
        this.taskObj = res;
        if (this.taskObj.Project != null) {
          this.projectName = this.taskObj.Project.ProjectName;
        }
        if (this.taskObj.AssignedUser != null) {
          this.userName = this.taskObj.AssignedUser.FirstName + " " + this.taskObj.AssignedUser.LastName;
          console.log(this.taskObj.ParentTaskName);
        }
        this.parentTaskName = this.taskObj.ParentTaskName;
      });
      // }
    }
  }


  ngOnInit() {
  }
  ngAfterViewInit() {
    fromEvent(document.getElementById("txtUsrSrch"), "input").pipe(map((e: KeyboardEvent) => (<HTMLInputElement>e.target).value)
      , debounceTime(100)
      , distinctUntilChanged()
      , switchMap((searchTerm) =>
        this.searchText(searchTerm)
      ))
      .subscribe(c => {
        console.log(c);
        this.lstItem = [];
        switch (this.searchId) {
          case 1: {
            this.lstProjects = c;
            this.lstProjects.forEach(project => {
              this.mv = new ModelView();
              this.mv.Name = project.ProjectName;
              this.mv.Type = project;
              this.lstItem.push(this.mv);
              // console.log((project.ProjectName));
            });
            break;
          }
          case 2: {
            this.lstParentTask = c;
            this.lstParentTask.forEach(parntTask => {
              this.mv = new ModelView();
              this.mv.Name = parntTask.TaskName;
              this.mv.Type = parntTask;
              this.lstItem.push(this.mv);
            });
            break;
          }
          case 3: {
            this.lstUsers = c;
            this.lstUsers.forEach(user => {
              this.mv = new ModelView();
              this.mv.Name = user.FirstName + " " + user.LastName;
              this.mv.Type = user;
              this.lstItem.push(this.mv);
            });
            break;
          }
        }
      })
  }
  onPrjSearchClick() {
    this.lstItem = [];
    this.searchId = 1;
    this.searchTitle = "Search Project(s)";
    this._projService.GetProjectList().subscribe(res => {
      this.lstProjects = res;
      this.lstProjects.forEach(project => {
        this.mv = new ModelView();
        this.mv.Name = project.ProjectName;
        this.mv.Type = project;
        this.lstItem.push(this.mv);
        // console.log((project.ProjectName));
      });
    });
  }
  onPrntTskSearchClick() {
    this.lstItem = [];
    this.searchId = 2;
    this.searchTitle = "Search Parent Task(s)";
    this._service.GetTasksList().subscribe(res => {
      this.lstParentTask = res;
      this.lstParentTask.forEach(parntTask => {
        if (parntTask.IsParentTask) {
          this.mv = new ModelView();
          this.mv.Name = parntTask.TaskName;
          this.mv.Type = parntTask;
          this.lstItem.push(this.mv);
        }
      });
    });
    //this.lstItem = ["E-Remittance", "Transmission", "Integrated Receivables", "Global VR"];
  }
  onUserSearchClick() {
    this.lstItem = [];
    this.searchId = 3;
    this.searchTitle = "Search User(s)";
    this._userService.GetUserList().subscribe(res => {
      this.lstUsers = res;
      this.lstUsers.forEach(user => {
        this.mv = new ModelView();
        this.mv.Name = user.FirstName + " " + user.LastName;
        this.mv.Type = user;
        this.lstItem.push(this.mv);
        // console.log((project.ProjectName));
      });

    });
    // this.lstItem = ["Abhinav", "Manish", "Vivek", "Deepak", "Akash", "Gaurav", "Shreyas", "Amit"];
  }

  handleChange(evt) {
    switch (this.searchId) {
      case 1: {
        this.selectedproject = evt;
        console.log(evt);
        break;
      }
      case 2: {
        this.selectedparenttask = evt;
        console.log(evt);
        break;
      }
      case 3: {
        this.selecteduser = evt;
        console.log(evt);
        break;
      }
    }
  }

  onSelection() {
    switch (this.searchId) {
      case 1: {
        if (this.selectedproject.ProjectID > 0) {
          this.projectName = this.selectedproject.ProjectName;
          this.taskObj.Project = this.selectedproject;
        }
        break;
      }
      case 2: {
        if (this.selectedparenttask.TaskID > 0) {
          this.parentTaskName = this.selectedparenttask.TaskName;
          this.taskObj.ParentTaskID = this.selectedparenttask.TaskID;
          this.taskObj.ParentTaskName = this.selectedparenttask.TaskName;
        }
        break;
      }
      case 3: {
        if (this.selecteduser.UserId > 0) {
          this.userName = this.selecteduser.FirstName + " " + this.selecteduser.LastName;
          this.taskObj.AssignedUser = this.selecteduser;
        }
        break;
      }
    }
    console.log('on selection');
  }

  ValidateDate() {
    console.log('validate date');
    this.dateIsValid = false;
    if (this._datePipe.transform(this.taskObj.StartDate, 'yyyy-MM-dd') <= this._datePipe.transform(this.taskObj.EndDate, 'yyyy-MM-dd')) {
      this.dateIsValid = true;
    }
    console.log(this.dateIsValid);
  }

  searchText(searchTerm): Observable<any> {
    switch (this.searchId) {
      case 1: {
        return this._projService.GetProject(searchTerm);
      }
      case 2:
      {
        return this._service.GetTask(searchTerm); // TODO - this._service.getparentTaskByName(searchTerm);
      }
      case 3: {
        return this._userService.GetUser(searchTerm);

      }
    }
  }

  addTask() {
    if (!this.isPrntTskChckd) {
      this._service.AddTask(this.taskObj).subscribe(res => { this.responseMsg = res; });
    }
    else {
      //this._service.addparentTask(this.taskObj).subscribe(res => this.responseMsg = res);
    }
  }

  checkChange() {
    this.taskObj.StartDate = null;
    this.taskObj.EndDate = null;
    this.taskObj.Priority = null;
    this.taskObj.ParentTaskID = 0;
    this.taskObj.ParentTaskName = "";
    this.taskObj.AssignedUser = null;
    this.parentTaskName = "";
    this.userName = "";
  }
}

class ModelView {
  Name: string;
  Type: any;
}

