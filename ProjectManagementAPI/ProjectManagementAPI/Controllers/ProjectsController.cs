using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace ProjectManagementAPI.Controllers
{
    public class ProjectsController : ApiController, IProjects
    {
        ProjectManagerDBEntities _dbContex = new ProjectManagerDBEntities();
        [ActionName("GetProjectList")]
        public IHttpActionResult Get()
        {
            var projectDetails = (from p in _dbContex.Projects
                                  select new ProjectDetails()
                                  {
                                      ProjectId = p.ProjectId,
                                      ProjectName = p.ProjectName,
                                      StartDate = p.StartDate,
                                      EndDate = p.EndDate,
                                      Priority = p.Priority ?? 0,
                                      NumberOfTasks = _dbContex.Tasks.Count(x => x.ProjectId == p.ProjectId),
                                      IsCompleted = p.EndDate <= DateTime.Now ? true : false,
                                      ManagerId=_dbContex.Users.Where(x=>x.ProjectId==p.ProjectId).FirstOrDefault().UserId  ,
                                      ManagerName= _dbContex.Users.Where(x => x.ProjectId == p.ProjectId).FirstOrDefault().FirstName +" "+ _dbContex.Users.Where(x => x.ProjectId == p.ProjectId).FirstOrDefault().LastName,
                                  }).AsQueryable();

            if (projectDetails != null)
            {
                return Ok(projectDetails);
            }
            return NotFound();

        }

        private string GetManagerName(int projectId)
        {
            var user = _dbContex.Users.Where(x => x.ProjectId == projectId).FirstOrDefault();
            return user?.FirstName + " " + user?.LastName;
        }
        // GET api/values/5
        [ActionName("GetProject")]
        public IHttpActionResult Get(int id)
        {
            var filterProject = _dbContex.Projects
                                         .Where(p => p.ProjectId == id)
                                        .Select(p => new ProjectDetails()
                                        {
                                            ProjectId = p.ProjectId,
                                            ProjectName = p.ProjectName,
                                            StartDate = p.StartDate,
                                            EndDate = p.EndDate,
                                            Priority = p.Priority ?? 0,
                                            NumberOfTasks = _dbContex.Tasks.Count(x => x.ProjectId == p.ProjectId),
                                            IsCompleted = p.EndDate <= DateTime.Now ? true : false,
                                            ManagerId = _dbContex.Users.Where(x => x.ProjectId == p.ProjectId).FirstOrDefault().UserId,
                                            ManagerName = _dbContex.Users.Where(x => x.ProjectId == p.ProjectId).FirstOrDefault().FirstName + " " + _dbContex.Users.Where(x => x.ProjectId == p.ProjectId).FirstOrDefault().LastName,

                                        });
            if (filterProject != null)
            {
                return Ok(filterProject);
            }
            return NotFound();
        }
        [ActionName("AddProject")]
        public IHttpActionResult Post([FromBody]Entities.Project proj)
        {
            try
            {
                var newProj = new Models.Project()
                {
                    ProjectName = proj.ProjectName,
                    Priority = proj.Priority,
                    StartDate = proj.StartDate,
                    EndDate = proj.EndDate,

                };
                if (!CheckProjectAlreadyExist(proj)) // Check User already exist
                {
                    _dbContex.Projects.Add(newProj);

                    _dbContex.SaveChanges();
                    if (proj.ManagerId != null)
                    {
                        var user = _dbContex.Users.Where(u => u.UserId == proj.ManagerId).FirstOrDefault();
                        if (user != null)
                        {
                            user.ProjectId = newProj.ProjectId;
                        }
                        _dbContex.SaveChanges();
                    }
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        private bool CheckProjectAlreadyExist(Entities.Project proj)
        {
            return _dbContex.Projects.Any(p => p.ProjectName.ToLower().Equals(proj.ProjectName.ToLower()));
        }
        // PUT: api/Project/5
        [ActionName("UpdateProject")]
        public IHttpActionResult Put(int id, [FromBody]Entities.Project proj)
        {
            try
            {
                var filter = _dbContex.Projects.FirstOrDefault(x => x.ProjectId == id);
                if (filter != null)
                {
                    filter.ProjectName = proj.ProjectName;
                    filter.StartDate = proj.StartDate;
                    filter.EndDate = proj.EndDate;
                    filter.Priority = proj.Priority;
                    if (proj.ManagerId != null)
                    {
                        var filterMgr = _dbContex.Users.FirstOrDefault(u => u.UserId == proj.ManagerId);
                        if (filterMgr != null)
                        {
                            filterMgr.ProjectId = id;
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
        [ActionName("DeleteProject")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var delProj = _dbContex.Projects.FirstOrDefault(x => x.ProjectId == id);
                if (delProj != null)
                {
                    var result = _dbContex.Projects.Remove(delProj);
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

