import { Project } from "./Project";
import { User } from "./User";

export class ProjTask {
    TaskID: number;
    TaskName: string;
    ParentTaskName: string;
    ParentTaskID: number;
    Priority: number;
    StartDate: Date;
    EndDate: Date;
    Project :Project;
    AssignedUser:User;
    IsParentTask:boolean;
    Status:string;
}