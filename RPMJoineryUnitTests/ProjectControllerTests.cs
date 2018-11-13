using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
            //Arrange
            //var fakeHttpContext = new Mock<HttpContextBase>();
            //var fakeIdentity = new GenericIdentity("User");
            //var principal = new GenericPrincipal(fakeIdentity, null);

            //fakeHttpContext.Setup(t => t.User).Returns(principal);
            //var controllerContext = new Mock<ControllerContext>();
            //controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            //ProjectsController controller = new ProjectsController();

            //Set ControllerContext with fake context
            //controller.ControllerContext = controllerContext.Object;

            //Act
            //ViewResult result = controller.Index() as ViewResult;

            //Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateProject_NoImages_NotNull()
        {
            //Arrange
            string[] typeArr = { "bathroom", "kitchen" };
            var project = new Project { Id = '1', UserID = Guid.NewGuid(), Title = "Project Title", Description = "PRoject Description", Type = "", Details = "Details string" };

            var mock = new Mock<ProjectsController>();
            mock.CallBase = true;
            mock.Setup(m => m.AddProject(project)).Returns(true);
            mock.Setup(m => m.SaveDbChanges()).Returns(true);

            //Act
            var result = mock.Object.Create(null, typeArr, null, null, null, null, null) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProject_NullProjectParameters_Null()
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
        
    }
}
