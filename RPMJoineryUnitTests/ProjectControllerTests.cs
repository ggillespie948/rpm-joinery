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

namespace RPMJoineryUnitTests
{
    [TestClass]
    public class ProjectControllerTests
    {
        [TestMethod]
        public void TestMethodIndex()
        {

            //Arrange
            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            ProjectsController controller = new ProjectsController();

                //Set ControllerContext with fake context
            controller.ControllerContext = controllerContext.Object;


            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
