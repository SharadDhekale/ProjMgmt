import { Component, OnInit } from '@angular/core';
import { Project } from '../Model/Project';
import { ProjTask } from '../Model/ProjTask';
import {ProjectService} from '../Services/ProjectService';
import {ProjTaskService} from '../Services/TaskService';
import {SortPipe} from '../PipeExtension/StringSort.pipe';
import {DateSortPipe} from '../PipeExtension/DateSortPipe';
import { Router } from '@angular/router';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-view-tasks',
  templateUrl: './view-tasks.component.html',
  styleUrls: ['./view-tasks.component.css'],
  providers: [SortPipe, DateSortPipe]
})
export class ViewTasksComponent implements OnInit {

  searchTitle: string;
  lstItem: Project[];
  lstProjects: Project[];
  selectedproject: Project;
  projectName: string;
  lstTasks: ProjTask[];
  templstTasks: ProjTask[];

  constructor(private _projservice: ProjectService,
    private _taskService:ProjTaskService,   
    private _router: Router,
    private _datePipe: DatePipe, private _sortPipe: SortPipe, private _dateSort: DateSortPipe) {
    this.lstTasks = [];
    this.templstTasks = [];
    this.getAllTasks();
  }


  getAllTasks() {
    this._taskService.GetTasksList().subscribe(res => {
      this.templstTasks = res;
      this.templstTasks.forEach(task => {
        if (!task.IsParentTask) {
          this.lstTasks.push(task);
        }
      });
    })
  }
  ngOnInit() {
  }

  ngAfterViewInit() {
    fromEvent(document.getElementById("txtUsrSrch"), "input").pipe(map((e: KeyboardEvent) => (<HTMLInputElement>e.target).value)
      , debounceTime(100)
      , distinctUntilChanged()
      , switchMap((searchTerm) =>
        this._projservice.GetProjectByName(searchTerm)
      ))
      .subscribe(c => {
        this.lstItem = c;
      });
  }
  onPrjSearchClick() {
    this.searchTitle = "Search Projects";
    this.lstItem = [];
    this._projservice.GetProjectList().subscribe(res => this.lstItem = res);
  }

  handleChange(evt) {
    this.selectedproject = evt;
    /// console.log(evt);
  }


  onSelection() {
    if (this.selectedproject.ProjectID > 0) {
      this.projectName = this.selectedproject.ProjectName;
      this.lstTasks = [];
      this._taskService.GetTaskByProjectId(this.selectedproject.ProjectID).subscribe(res => this.lstTasks = res);
    }
  }

  editTask(obj) {
    this._router.navigate(['updatetask', obj.TaskID]);
  }

  endTask(obj) {

    if (this._datePipe.transform(obj.StartDate, 'yyyy-MM-dd') > this._datePipe.transform(new Date(), 'yyyy-MM-dd')) {
      obj.StartDate = this._datePipe.transform(new Date(), 'yyyy-MM-dd');
    }

    obj.EndDate = this._datePipe.transform(new Date(), 'yyyy-MM-dd');

    this._taskService.AddTask(obj).subscribe(r => {
      if (this.selectedproject != null) {
        this._taskService.GetTaskByProjectId(this.selectedproject.ProjectID).subscribe(res => this.lstTasks = res);
      }
      else {
        this.lstTasks=[];
        this.getAllTasks();
      }
    })
  }

  sortByPriority() {
    this.lstTasks = this._sortPipe.transform(this.lstTasks, "Priority");
  }

  sortByStarDate() {
    this.lstTasks = this._dateSort.transform(this.lstTasks, "StartDate");
  }
  sortByEndDate() {
    this.lstTasks = this._dateSort.transform(this.lstTasks, "EndDate");
  }
}
