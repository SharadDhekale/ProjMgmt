using System.Collections.Generic;
using System.Linq;

namespace JobOpenings
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class JobOpenings : IJobOpenings
    {
        private List<Jobs> _availbleJobsList = new List<Jobs>();

        public JobOpenings()
        {
            _availbleJobsList = new List<Jobs>();
            _availbleJobsList.Add(new Jobs
                        {
                          Category ="IT",
                          Role="DBA",
                          Title="DB Administrator",
                          Location="Chicago"
                        });
            _availbleJobsList.Add(new Jobs
            {
                Category = "IT",
                Role = "Full Stack Developer",
                Title = "Associate",
                Location = "Plano"
            });
            _availbleJobsList.Add(new Jobs
            {
                Category = "IT",
                Role = "Full Stack Developer",
                Title = "VP",
                Location = "Portland"
            });
            _availbleJobsList.Add(new Jobs
            {
                Category = "Mechanical",
                Role = "Operator",
                Title = "Trainee",
                Location = "Dallas"
            });
            _availbleJobsList.Add(new Jobs
            {
                Category = "Mechanical",
                Role = "Designer",
                Title = "Team Lead",
                Location = "Dallas"
            });
        }
        /// <summary>
        /// return available list of jobs
        /// </summary>
        /// <returns></returns>
         public List<Jobs> GetAvailbleJobList()
        {
            return _availbleJobsList;
        }
        /// <summary>
        /// Filter List for requested Job role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<Jobs> GetJobsByRole(string role)
        {
            var filteredList = _availbleJobsList
                .Where(j => j.Role.ToLower()
                             .Contains(role.ToLower()))
                .ToList();
            return filteredList;
        }
    }
}
