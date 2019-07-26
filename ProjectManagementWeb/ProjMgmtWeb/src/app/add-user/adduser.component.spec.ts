import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AddUserComponent } from '../add-user/add-user.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { UserService } from '../Services/UserService';
import { DatePipe } from '@angular/common';
import { SortPipe } from '../PipeExtension/StringSort.pipe';
import { DateSortPipe } from '../PipeExtension/DateSortPipe';
import { Observable, of } from 'rxjs';
import { User } from '../Model/User';

describe('AdduserComponent', () => {
  let component: AddUserComponent;
  let fixture: ComponentFixture<AddUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule, RouterTestingModule],
      declarations: [AddUserComponent]
    })
      .overrideComponent(AddUserComponent, {
        set: {
          providers: [{ provide: UserService, useClass: MockUserService }, { provide: DatePipe }, { provide: SortPipe },
          { provide: DateSortPipe }]
        }
      })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('adduser test', () => {
    component.addUser();
    expect(component.ResponseMsg).toEqual("User added successfully");
    expect(component.lstUsers.length).toEqual(2);
  });
  it('delete user test', () => {
    let user:User;
    user=new User();
    user.UserId=100;
    user.FirstName="Manish";
    component.delete(user);
    expect(component.ResponseMsg).toEqual("User deleted successfully");
    expect(component.lstUsers.length).toEqual(2);
  });
});

class MockUserService {
    AddUser(): Observable<any> {
    return of("User added successfully");
  }
  DeleteUser(user:User): Observable<any> {
    return of("User deleted successfully");
  }
  GetUserList(): Observable<any> {
    return of([{ 'UserId': 101, 'FirstName': 'Sharad', 'LastName': 'Dhekale' },
    { 'UserId': 102, 'FirstName': 'Sara', 'LastName': 'Patel' }]);
  }
}