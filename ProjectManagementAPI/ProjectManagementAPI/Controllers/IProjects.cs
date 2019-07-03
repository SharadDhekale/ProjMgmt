using System.Web.Http;

namespace ProjectManagementAPI.Controllers
{
    public interface IProjects
    {
        /// <summary>
        /// Return all the available projects list
        /// </summary>
        /// <returns></returns>
        IHttpActionResult Get();
        /// <summary>
        /// Return specific project details based on the requested id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IHttpActionResult Get(int id);
        /// <summary>
        /// Add new Project
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        IHttpActionResult Post([FromBody]Entities.ProjectDetails proj);
        /// <summary>
        /// Update the requested project details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proj"></param>
        /// <returns></returns>
        IHttpActionResult Put(int id, [FromBody]Entities.ProjectDetails proj);

        /// <summary>
        /// Delete the requested project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IHttpActionResult Delete(int id);
    }
}
