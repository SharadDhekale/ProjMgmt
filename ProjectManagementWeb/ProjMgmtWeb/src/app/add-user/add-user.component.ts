import { Component, OnInit, AfterViewInit } from '@angular/core';
import { UserService } from '../Services/UserService';
import { User } from '../Model/User';
import { Router } from '@angular/router';
import { fromEvent } from 'rxjs';
import { map, filter, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators/';
import { SortPipe } from '../PipeExtension/StringSort.pipe';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit, AfterViewInit {

 
  firstNm: string;
  lastNm: string;
  empId: number;
  usrObj: User = new User();
  ResponseMsg: string;
  lstUsers: User[];
  buttonText: string = "";
  searchText: string = "";
  constructor(private _userService: UserService, private _router: Router, private _sort: SortPipe) {
    this.getallUser();
    this.buttonText = "Add";
    //console.log($("#srchProject").text);


  }

  ngOnInit() {
  }
  ngAfterViewInit() {
    fromEvent(document.getElementById("srchProject"), "input").pipe(map((e: KeyboardEvent) => (<HTMLInputElement>e.target).value)
      , debounceTime(100)
      , distinctUntilChanged()
      , switchMap((searchTerm) =>
        this._userService.GetUser(searchTerm)
      ))
      .subscribe(c => {
         console.log(c);
        this.lstUsers = c;
      });
  }

  getallUser() {
    this._userService.GetUserList().subscribe(res => {
      this.lstUsers = res;
    });
  }
  addUser() {
    // this.usrObj.FirstName = this.firstNm;
    // this.usrObj.LastName = this.lastNm;
    // this.usrObj.EmployeeId = this.empId;
    if ( this.buttonText = "Update"){
      this._userService.UpdateUser(3,this.usrObj).subscribe(r => {
        this.ResponseMsg = r;
        this.getallUser();
      });
    }else{
    this._userService.AddUser(this.usrObj).subscribe(r => {
      this.ResponseMsg = r;
      this.getallUser();
    });
  }
  }
  edit(obj) {
    this.buttonText = "Update";
    this.usrObj = obj;
  }
  delete(obj) {
    this._userService.DeleteUser(obj).subscribe(r => {
      this.ResponseMsg = r;
      this.getallUser();
    });
  }

  reset() {
    this.usrObj = new User();
  }

  SortByFN() {
    this.lstUsers = this._sort.transform(this.lstUsers, "FirstName");
  }

  SortByLN() {
    this.lstUsers = this._sort.transform(this.lstUsers, "LastName");
  }
  SortByID() {
    this.lstUsers = this._sort.transform(this.lstUsers, "EmployeeId");
  }
}
