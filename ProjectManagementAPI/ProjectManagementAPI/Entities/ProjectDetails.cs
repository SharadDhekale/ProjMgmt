using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAPI.Entities
{
    public class ProjectDetails
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public int NumberOfTasks { get; set; }
        public bool IsCompleted { get; set; }
        public User Manager { get; set; }
    }
}
