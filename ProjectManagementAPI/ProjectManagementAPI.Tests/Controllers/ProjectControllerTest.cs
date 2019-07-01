using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagementAPI.Controllers;
using Moq;
using ProjectManagementAPI.Entities;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Results;

namespace ProjectManagementAPI.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest: ApiController
    {
        [TestMethod]
        public void TestProjectById()
        {
            ProjectsController proj = new ProjectsController();
            var projMoq = new Mock<IProjects>();
          
            var testProj = new ProjectDetails() { ProjectId = 2,
                ProjectName = "TestProj",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                IsCompleted = false
            };
           // int projId = 2;
            projMoq.Setup(x => x.Get(It.IsAny<int>())).Returns(Ok(testProj));
            var result = projMoq.Object.Get(2);

            var chk = result as OkNegotiatedContentResult<ProjectDetails>;
            Assert.AreEqual("TestProj", chk.Content.ProjectName);
        }
    }
}
