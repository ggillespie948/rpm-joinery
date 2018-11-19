using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RPMJoinery;
using RPMJoinery.Controllers;
using RPMJoinery.Models;

namespace RPMJoineryUnitTests
{
    [TestClass]
    public class ProjectControllerTests
    {
        [TestMethod]
        public void TestMethodIndex()
        {
            // Arrange
            List<Project> projects = new List<Project>();
            var project1 = new Project { Id = '1', UserID = Guid.NewGuid(), Title = "Project Title 1", Description = "PRoject Description", Type = "", Details = "Details string" };
            var project2 = new Project { Id = '2', UserID = Guid.NewGuid(), Title = "Project Title 2", Description = "PRoject Description", Type = "", Details = "Details string" };
            var project3 = new Project { Id = '3', UserID = Guid.NewGuid(), Title = "Project Title 3", Description = "PRoject Description", Type = "", Details = "Details string" };
            projects.Add(project1);
            projects.Add(project2);
            projects.Add(project3);

            var mock = new Mock<ProjectsController>();
            mock.Setup(m => m.IsUserAuthenticated).Returns(true);
            mock.Setup(m => m.GetProjects()).Returns(projects);
            mock.CallBase = true;

            // Act
            var result = mock.Object.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void POST_CreateProject_NoImages_NotNull()
        {
            //Arrange
            string[] typeArr = { "bathroom", "kitchen" };
            var project = new Project { Id = '1', UserID = Guid.NewGuid(), Title = "Project Title", Description = "PRoject Description", Type = "", Details = "Details string" };

            var mock = new Mock<ProjectsController>();
            mock.CallBase = true;
            mock.Setup(m => m.AddProject(project)).Returns(true);
            mock.Setup(m => m.SaveDbChanges()).Returns(true);

            //Act
            var result = mock.Object.Create(project, typeArr, null, null, null, null, null) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GET_CreateProject_NoImages_NotNull()
        {
            //Arrange
            var mock = new Mock<ProjectsController>();
            mock.CallBase = true;
            mock.Setup(m => m.GetCurrentUserID()).Returns(Guid.NewGuid());

            //Act
            var result = mock.Object.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void CreateProject_NullTags_NotNull()
        {
            //Arrange
            var project = new Project { Id = '1', UserID = Guid.NewGuid(), Title = null, Description = "PRoject Description", Type = "", Details = "Details string" };

            var mock = new Mock<ProjectsController>();
            mock.Setup(m => m.AddProject(project)).Returns(true);

            //Act
            var result = mock.Object.Create(project, null, null, null, null, null, null);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GET_EditProject_NotNull()
        {
            // Arrange
            List<Project> projects = new List<Project>();
            var project1 = new Project { Id = '1', UserID = Guid.NewGuid(), Title = "Project Title 1", Description = "PRoject Description", Type = "", Details = "Details string" };
            var mock = new Mock<ProjectsController>();
            mock.Setup(m => m.IsUserAuthenticated).Returns(true);
            mock.Setup(m => m.FindProject(1)).Returns(project1);
            mock.CallBase = true;

            // Act
            var result = mock.Object.Edit(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void POST_EditProject_NotNull()
        {
            // Arrange
            string[] typeArr = { "bathroom", "kitchen" };
            List<Project> projects = new List<Project>();
            var project1 = new Project { Id = '1', UserID = Guid.NewGuid(), Title = "Project Title 1", Description = "PRoject Description", Type = "", Details = "Details string", ImgFilePath="/fake/path" };
            var mock = new Mock<ProjectsController>();
            mock.Setup(m => m.IsUserAuthenticated).Returns(true);
            mock.Setup(m => m.SaveDbChanges()).Returns(true);
            mock.CallBase = true;

            // Act
            var result = mock.Object.Edit(project1, typeArr) as RedirectToRouteResult; 

            //Assert
            Assert.IsNotNull(result);
        }

  
    }
}
