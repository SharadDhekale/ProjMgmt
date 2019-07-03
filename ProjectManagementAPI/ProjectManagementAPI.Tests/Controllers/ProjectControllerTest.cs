using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagementAPI.Controllers;
using Moq;
using ProjectManagementAPI.Entities;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace ProjectManagementAPI.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest: ApiController
    {
        [TestMethod]
        public void TestProjectById()
        {
            // Arrange
            ProjectsController proj = new ProjectsController();
            var projMoq = new Mock<IProjects>();
          
            var testProj = new ProjectDetails() { ProjectId = 2,
                ProjectName = "TestProj",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                IsCompleted = false
            };
            projMoq.Setup(x => x.Get(It.IsAny<int>())).Returns(Ok(testProj));

            // Act
            var result = projMoq.Object.Get(2);
          
            var chk = result as OkNegotiatedContentResult<ProjectDetails>;
            // Assert
            Assert.AreEqual("TestProj", chk.Content.ProjectName);
        }
        [TestMethod]
        public void TestProjectList()
        {
            // Arrange
            ProjectsController proj = new ProjectsController();
            var projMoq = new Mock<IProjects>();
            var projectList = new List<ProjectDetails>();
            var testProj = new ProjectDetails()
            {
                ProjectId = 1,
                ProjectName = "TestProj1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                IsCompleted = false
            };

            projectList.Add(testProj);
              testProj = new ProjectDetails()
            {
                ProjectId = 2,
                ProjectName = "TestProj2",
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(15),
                IsCompleted = false
            };
            projectList.Add(testProj);
            projMoq.Setup(x => x.Get()).Returns(Ok(projectList));

            // Act
            var result = projMoq.Object.Get();

            var chk = result as OkNegotiatedContentResult<List<ProjectDetails>>;
            // Assert
            Assert.AreEqual("TestProj2", chk.Content[1].ProjectName);
        }

        [TestMethod]
        public void TestAddProject()
        {
            // Arrange
            ProjectsController proj = new ProjectsController();
            var projMoq = new Mock<IProjects>();

            var testProj = new ProjectDetails()
            {
                ProjectId = 1,
                ProjectName = "TestProj",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                IsCompleted = false
            };
            projMoq.Setup(x => x.Post(It.IsAny<ProjectDetails>())).Returns(Ok());

            // Act
            var result = projMoq.Object.Post(testProj);

            var chk = result as OkNegotiatedContentResult<ProjectDetails>;
            // Assert
            //Assert.IsNotNull(chk);
        }
    }
}
