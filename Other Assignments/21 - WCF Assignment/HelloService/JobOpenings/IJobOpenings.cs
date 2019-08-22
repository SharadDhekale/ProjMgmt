using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace JobOpenings
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IJobOpenings
    {

        [OperationContract]
        List<Jobs> GetAvailbleJobList();
        [OperationContract]
        List<Jobs> GetJobsByRole(string role);
         
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Jobs
    {
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Location { get; set; }
    }

}
