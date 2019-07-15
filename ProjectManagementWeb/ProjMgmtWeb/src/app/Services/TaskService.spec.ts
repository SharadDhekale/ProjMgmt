import { TestBed, inject } from '@angular/core/testing';

import { ProjTaskService } from './TaskService';
import { HttpClientModule } from '@angular/common/http';

// Inject service dependency before each call
beforeEach(() => {
  TestBed.configureTestingModule({
    imports:[HttpClientModule],
    providers: [ProjTaskService]
  });

});
// Tasks service object should create
describe('ProjTaskService', () => {
   it('ProjTaskService should be created', inject([ProjTaskService], (service: ProjTaskService) => {
    expect(service).toBeTruthy();
  }));
});

describe('TaskListTest',()=>{
  
  it('should return available Lists of Tasks', inject([ProjTaskService], (service: ProjTaskService) => {
    expect(service).toBeTruthy();
    let taskslist= service.GetTasksList();
    // expect(taskslist).length>0;
  }));
});

describe('GetTaskDetails',()=>{
  let  taskId=0;
    it('should return Tasks details for requested id', inject([ProjTaskService], (service: ProjTaskService) => {
      expect(service).toBeTruthy();
      let taskslist= service.GetTask(1).subscribe(t=>{
      // taskId=t[0].TaskId;
      console.log(t);
      });
      expect(taskId).toBe(1);
    }));
   
  });