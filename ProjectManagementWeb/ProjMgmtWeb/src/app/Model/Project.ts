import { User } from "./User";

export class Project{

    ProjectID:number;
    ProjectName:string;
    NumberOfTasks:number;
    CompletedTasks:number;
    IsCompleted:boolean;
    StartDate:Date;
    EndDate:Date;
    Priority:number;
    Manager:User;
}