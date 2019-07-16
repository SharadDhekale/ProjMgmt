using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagementAPI.Controllers
{
    public class TasksController : ApiController
    {
        ProjectManagerDBEntities _dbContex = new ProjectManagerDBEntities();

        [ActionName("GetTasksList")]
        public IHttpActionResult Get()
        {
            var projectTaskList = (from t in _dbContex.Tasks
                                   select
                                          new Entities.ProjTask()
                                          {
                                              TaskId = t.TaskId,
                                              ParentTaskId = t.ParentId,
                                              //ParentTask = GetTask(0, t.ParentId).Single(x=> new Entities.ProjTask() { }),
                                              TaskName = t.TaskName,
                                              Priority = t.Priority,
                                              StartDate = t.StartDate,
                                              EndDate = t.EndDate ,
                                          }).AsQueryable();



            if (projectTaskList != null)
            {
                return Ok(projectTaskList);
            }
            return NotFound();

        }

        private IQueryable<Entities.ProjTask> GetTask(int? taskId = 0, int? parentId = 0)
        {
            IQueryable<Entities.ProjTask> filterTask = null;
            if (parentId != 0)
            {
                var prentTaskName = _dbContex.ParentTasks.FirstOrDefault(x => x.ParentId == parentId)?.ParentTask1;
                filterTask = _dbContex.Tasks
                                          .Where(t => t.TaskName.ToLower() == prentTaskName.ToLower())
                                         .Select(t => new Entities.ProjTask()
                                         {
                                             TaskId = t.TaskId,
                                             ParentTaskId = t.ParentId,
                                             TaskName = t.TaskName,
                                             Priority = t.Priority,
                                             StartDate = t.StartDate,
                                             EndDate = t.EndDate,

                                         }).AsQueryable();
            }
            else
            {
                filterTask = _dbContex.Tasks
                                          .Where(t => t.TaskId == taskId)
                                         .Select(t => new Entities.ProjTask()
                                         {
                                             TaskId = t.TaskId,
                                             ParentTaskId = t.ParentId,
                                             TaskName = t.TaskName,
                                             Priority = t.Priority,
                                             StartDate = t.StartDate,
                                             EndDate = t.EndDate,

                                         }).AsQueryable();
            }


            return filterTask;
        }
        // GET api/values/5
        [ActionName("GetTask")]
        public IHttpActionResult Get(int id)
        {
            var filterTask = this.GetTask(id);
            if (filterTask != null)
            {
                return Ok(filterTask);
            }
            return NotFound();
        }

        [ActionName("GetTaskByProjectId")]
        public IHttpActionResult GetTaskByProjectId(int id)
        {
            var filterTask = _dbContex.Tasks
                                           .Where(t => t.ProjectId == id)
                                          .Select(t => new Entities.ProjTask()
                                          {
                                              TaskId = t.TaskId,
                                              ParentTaskId = t.ParentId,
                                              TaskName = t.TaskName,
                                              Priority = t.Priority,
                                              StartDate = t.StartDate,
                                              EndDate = t.EndDate,

                                          }).AsQueryable();
            if (filterTask != null)
            {
                return Ok(filterTask);
            }
            return NotFound();
        }
        [ActionName("AddTask")]
        public IHttpActionResult Post([FromBody]Entities.ProjTask task)
        {
            try
            {
                var newTask = new Models.Task()
                {
                    TaskName = task.TaskName,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    Priority = task.Priority,
                    ParentId = task.ParentTaskId
                };
                _dbContex.Tasks.Add(newTask);
                if (task.IsParentTask)
                {
                    var parent = new Models.ParentTask()
                    {
                        ParentTask1 = task.TaskName
                    };
                    _dbContex.ParentTasks.Add(parent);
                }
                _dbContex.SaveChanges();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        // PUT: api/Project/5
        [ActionName("UpdateTask")]
        public IHttpActionResult Put(int id, [FromBody]Entities.ProjTask projTask)
        {
            try
            {
                var filter = _dbContex.Tasks.FirstOrDefault(x => x.TaskId == id);
                if (filter != null)
                {
                    filter.TaskName = projTask.TaskName;
                    filter.StartDate = projTask.StartDate;
                    filter.EndDate = projTask.EndDate;
                    filter.Priority = projTask.Priority;

                    if (projTask.IsParentTask)
                    {
                        var checkSameParentAttached = _dbContex.ParentTasks.Where(x => x.ParentTask1.ToLower() == projTask.TaskName.ToLower()).FirstOrDefault();
                        if (checkSameParentAttached == null)
                        {
                            var newParent = new ParentTask() { ParentTask1 = projTask.TaskName };
                            _dbContex.ParentTasks.Add(newParent);
                            _dbContex.SaveChanges();
                            filter.ParentId = newParent.ParentId;
                        }
                        else
                        {
                            filter.ParentId = projTask.ParentTaskId;
                        }

                    }

                    _dbContex.SaveChanges();

                    return Ok();

                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Project/5
        [ActionName("DeleteTask")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var delTask = _dbContex.Tasks.FirstOrDefault(x => x.TaskId == id);
                if (delTask != null)
                {
                    var result = _dbContex.Tasks.Remove(delTask);
                    _dbContex.SaveChanges();
                    if (result != null)
                        return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

