using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAPI.Entities
{
    public class ProjTask
    {
        /*
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
             */
        public int TaskId { get; set; }
        public Project Project { get; set; }
        public string TaskName { get; set; }
        public bool IsParentTask { get; set; }
        public int? Priority { get; set; } = 0;
        public int? ParentTaskId { get; set; }
        public ProjTask ParentTask { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User AssignedUser { get; set; }
    }
}