import { TestBed, inject ,getTestBed} from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from  '@angular/common/http/testing';
//import { HttpClientModule } from '@angular/common/http';
import {UserService} from './UserService'
import { HttpClientModule } from '@angular/common/http';
// Inject service dependency before each call

describe('UserService', () => {
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports:[HttpClientModule],
        providers: [UserService]
      });
    });
  
    it('should be created', inject([UserService], (service: UserService) => {
      expect(service).toBeTruthy();
    }));
  });