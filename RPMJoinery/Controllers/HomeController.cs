using RPMJoinery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RestSharp;
using RestSharp.Authenticators;
using System.Web.Configuration;

namespace RPMJoinery.Controllers
{


    public class HomeController : Controller
    {
        private ProjectsWebContext db = new ProjectsWebContext();

        public ActionResult Index()
        {
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "RPM Joinery & Maintenance | Dundee Joinery Services";
            ViewBag.MetaDescription = "RPM Joinery & Maintenance provide top quality joinery and maintenance services to Dundee, Angus and Fife, all at a great price.";
            ViewBag.MetaKeywords = "RPM joinery maintenance, dundee joiner, dundee joinery, lochee joiner, broughty ferry joiner, rpm dundee, kitchen fitting dundee, decking dundee, reliable joiner dundee, flat renovation dundee, bathroom fitting dundee, windows, doors, property management";

            // Retrieve the 3 most recent projects from the data base and display in the preview of the index home page 
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
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "RPM Joinery & Maintenance | About ";
            ViewBag.MetaDescription = "RPM Joinery & Maintenance is a family run business that take great pride in providing the best possible service to meet all of our consumer's needs. We are proudly a part of the Dundee Trusted Trader Scheme, as well as the Angus Reputable Trader Scheme.";
            ViewBag.MetaKeywords = "RPM joinery maintenance, reliable joiner, trusted joiner dundee, quality cheap joiner dundee angus, about rpm dundee, trusted trader dundee joiner, trusted joiner angus, family run joiner dundee";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "RPM Joinery & Maintenance | Contact Us ";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Title = "RPM Joinery & Maintenance | Services ";
            ViewBag.MetaDescription = "";
            ViewBag.MetaKeywords = "";

            return View();
        }

        [HttpPost]
        public ActionResult SendMail(string name, string phone, string email, string message)
        {
            string domain = WebConfigurationManager.AppSettings["MailGunDomain"];
            string apikey = WebConfigurationManager.AppSettings["MailGunApiKey"];

            if(domain == null || domain == "" || apikey == null || apikey == "")
            {
                return RedirectToAction("Index");
            }


            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            apikey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", name + "-" + phone + " <mailgun@"+domain+">");
            request.AddParameter("to", "gzgillespie@outlook.com");
            request.AddParameter("subject", "RPM Joinery Contact Form Entry");
            request.AddParameter("text", message);
            request.Method = Method.POST;
            client.Execute(request);

            return RedirectToAction("About");
        }

        
    }
}