using RPMJoinery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RPMJoinery.Controllers
{


    public class HomeController : Controller
    {
        private ProjectsWebContext db = new ProjectsWebContext();

        public ActionResult Index()
        {
            List<Project> projects = new List<Project>();
            projects = db.Projects.ToList();
            projects.Reverse();

            List<Project> recentProjects = new List<Project>();


            int projectCounter = 0;             // this is the number of projects that are displayed on the front page
            foreach(Project proj in projects)
            {
                recentProjects.Add(proj);
                projectCounter++;

                if (projectCounter > 2)
                    break;
            }

            return View(recentProjects);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Your Services page";

            return View();
        }

        
    }
}