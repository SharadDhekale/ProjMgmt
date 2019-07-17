import { TestBed, inject ,getTestBed} from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from  '@angular/common/http/testing';
//import { HttpClientModule } from '@angular/common/http';
import {UserService} from './UserService'
// Inject service dependency before each call

describe('UserService', () => {

    let  injector: TestBed;
    
    let  myProvider: UserService;
    
    let  httpMock: HttpTestingController;
    
    beforeEach(() => {
    
    TestBed.configureTestingModule({
    
    imports: [HttpClientTestingModule],
    
    providers: [UserService]
    
    });
    
    injector = getTestBed();
    
    myProvider = injector.get(UserService);
    
    httpMock = injector.get(HttpTestingController);
    
    });
    
    afterEach  (()=>{
        httpMock.verify();
    });

    describe('GetUserDetails',()=>{
        const dummyUsers=[{
            UserId: 1,
            EmployeeId : 1234,
            FirstName :'Sharad',
            LastName :'Dhekale',
        }];

        myProvider.GetUser('Sharad').subscribe(users=>{
            console.log(users);
        });

        const req = httpMock.expectOne('http://localhost:2385/api/Users/');
        expect(req.request.method).toEqual('GET');
        req.flush(dummyUsers);
    });


    });
    
  