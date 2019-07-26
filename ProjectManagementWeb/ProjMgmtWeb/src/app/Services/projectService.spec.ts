import { TestBed, inject } from '@angular/core/testing';

import { ProjectService } from './ProjectService';
import { HttpClientModule } from '@angular/common/http';
import { Project } from '../Model/Project';

// Inject service dependency before each call
beforeEach(() => {
  TestBed.configureTestingModule({
    imports:[HttpClientModule],
    providers: [ProjectService]
  });

});
// Project service object should create
describe('ProjectService', () => {
   it('should be created', inject([ProjectService], (service: ProjectService) => {
    expect(service).toBeTruthy();
  }));
});

// Check Service should return available Project List
describe('ProjectListTest',()=>{
    let projectlist=null;
  it('should return available Projects List', inject([ProjectService], (service: ProjectService) => {
    expect(service).toBeTruthy();
    let result = service.GetProjectList();
   console.log(result);
  }));
  
});

// Check Service should return project details for the requested identifier
describe('ProjectById',()=>{
     let projectDetails=null;
     let projectId=1;
  it('should return Project details for requested ProjectId', inject([ProjectService], (service: ProjectService) => {
    expect(service).toBeTruthy();
     let resut= service.GetProject(1).subscribe(proj => {
        projectDetails=proj;
        projectId=proj[0].Id;
        console.log('ProjectId '+projectDetails[0].ProjectId);
     });
    //console.log(projectlist)
     expect(projectId).toBe(1)
  }));
});