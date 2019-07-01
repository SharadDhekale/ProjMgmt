using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagementAPI.Controllers
{
    public class UsersController : ApiController
    {
        ProjectManagerDBEntities _dbContex = new ProjectManagerDBEntities();

        // GET: api/Users
        [ActionName("GetUserList")]
        public IHttpActionResult Get()
        {
            var userList = _dbContex.Users
                                    .Select(u => new Entities.User
                                    {
                                        UserId = u.UserId,
                                        EmployeeId = u.EmployeeId,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        //ProjectId = u.ProjectId,
                                        //TaskId = u.TaskId
                                    });
            if (userList != null)
            {
                return Ok(userList);
            }
            return NotFound();

        }

        // GET: api/Users/5
        [ActionName("GetUser")]
        public IHttpActionResult Get(string Name)
        {
            var filteredUser = _dbContex.Users
                                        .Where(u => u.FirstName.ToLower().Contains(Name.ToLower()))
                                         .Select(u => new Entities.User()
                                         {
                                             UserId = u.UserId,
                                             EmployeeId = u.EmployeeId,
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                             //ProjectId = u.ProjectId,
                                             //TaskId = u.TaskId

                                         }).FirstOrDefault();
            if (filteredUser != null)
            {
                return Ok(filteredUser);
            }
            return NotFound();
        }

        // POST: api/Users
        [ActionName("AddUser")]
        public IHttpActionResult Post([FromBody]Entities.User user)
        {
            try
            {
                var newUser = new Models.User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeId = user.EmployeeId,
                    //ProjectId = user.ProjectId,
                    //TaskId = user.TaskId
                };
                if (!CheckUserAlreadyExist(user)) // Check User already exist
                {
                    _dbContex.Users.Add(newUser);

                    _dbContex.SaveChanges();
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

        private bool CheckUserAlreadyExist(Entities.User user)
        {
            return _dbContex.Users.Any(u => u.FirstName.ToLower().Equals(user.FirstName.ToLower())
                                    && u.LastName.ToLower().Equals(user.LastName.ToLower())
                                    && u.EmployeeId.ToLower().Equals(user.EmployeeId.ToLower())
                                    //&& u.ProjectId == user.ProjectId
                                    );
        }
        // PUT: api/Users/5
        [ActionName("UpdateUser")]
        public IHttpActionResult Put(int id, [FromBody]Entities.User user)
        {
            try
            {
                var filter = _dbContex.Users.FirstOrDefault(x => x.UserId == id);
                if (filter != null)
                {

                    filter.FirstName = user.FirstName;
                    filter.LastName = user.LastName;
                    filter.EmployeeId = user.EmployeeId;
                    //filter.ProjectId = user.ProjectId;
                    //filter.TaskId = user.TaskId;
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

        // DELETE: api/Users/5
        [ActionName("DeleteUser")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deluser = _dbContex.Users.FirstOrDefault(x => x.UserId == id);
                if (deluser != null)
                {
                    var result = _dbContex.Users.Remove(deluser);
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
