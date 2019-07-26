import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { ProjectService } from '../Services/ProjectService';
import { DatePipe } from '@angular/common';
import { SortPipe } from '../PipeExtension/StringSort.pipe';
import { DateSortPipe } from '../PipeExtension/DateSortPipe';
import { Observable, of } from 'rxjs';
import { Project } from '../Model/Project';
import { ProjectComponent } from './project.component';

describe('ProjectComponent', () => {
  let component: ProjectComponent;
  let fixture: ComponentFixture<ProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule, RouterTestingModule],
      declarations: [ProjectComponent]
    })
      .overrideComponent(ProjectComponent, {
        set: {
          providers: [{ provide: ProjectService, useClass: MockProjectService }, { provide: DatePipe }, { provide: SortPipe },
          { provide: DateSortPipe }]
        }
      })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
   
  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('get all projects test', () => {
    component.getAllProjects();
    expect(component.lstprojects.length).toEqual(2);
  });
  it('add project test',()=>{
    component.addProject();
    expect(component.responseMsg).toEqual("Project added successfully");
  })
   
});

class MockProjectService {

    GetProjectList():Observable<any>{
      return of([{'ProjectID':101,'ProjectName':'Taxrates Automation'},
                 {'ProjectID':102,'ProjectName':'TaxDocs Automation'}])
    }
    AddProject():Observable<any>{
      return of("Project added successfully");
    }
 
  }


