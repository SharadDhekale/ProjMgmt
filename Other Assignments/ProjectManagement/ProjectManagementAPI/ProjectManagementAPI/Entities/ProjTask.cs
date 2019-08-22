using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAPI.Entities
{
    public class ProjTask
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public bool IsParentTask { get; set; }
        public int? Priority { get; set; } = 0;
        public int? ParentId { get; set; }
        public ProjTask ParentTask { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
    }
}