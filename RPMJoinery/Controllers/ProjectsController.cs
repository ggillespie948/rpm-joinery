using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RPMJoinery.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace RPMJoinery.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectsWebContext db = new ProjectsWebContext();

        // GET: Projects
        public ActionResult Index()
        {
            if(TempData["searchTag"] == null)
            {
                TempData["searchTag"] = "All";
                TempData["searchTitle"] = "All";
            }


            ViewBag.SearchTitle = TempData["searchTitle"];

            switch (TempData["searchTag"].ToString())
            {
                case "All":
                    ViewBag.Title = "RPM Joinery & Maintenance | Projects";
                    ViewBag.MetaDescription = "RPM Joinery & Mainentance have completed a wide variety of different projects, below you can browse a range of previous projects we have completed for our loyl customers.";
                    ViewBag.MetaKeywords = "RPM joinery maintenance, rpm kitchens, rpm bathrooms, windows, doors, floors, fencing, decking, rpm small maintenance jobs, property management. rpm projects, rpm example work, rpm portfolio";
                    break;

                case "Kitchen":
                    ViewBag.Title = "RPM Joinery & Maintenance | Kitchens";
                    ViewBag.MetaDescription = "Complete kitchen installations by RPM Joinery & Maintenance Dundee, Angus and Perth.";
                    ViewBag.MetaKeywords = "RPM joinery maintenance, rpm kitchens, kitchen installation dundee, new kitchen dundee, dundee kitchen fitter";
                    break;

                case "Bathroom":
                    ViewBag.Title = "RPM Joinery & Maintenance | Bathrooms";
                    ViewBag.MetaDescription = "Complete bathroom installations by RPM Joinery & Maintenance Dundee, Angus and Perth.";
                    ViewBag.MetaKeywords = "RPM joinery maintenance, rpm bathrooms, bathroom installation dundee, new bathroom dundee, dundee bathroom wetwall";
                    break;

                case "Windows/Doors":
                    ViewBag.Title = "RPM Joinery & Maintenance | Windows and Doors";
                    ViewBag.MetaDescription = "Complete door and window installations by RPM Joinery & Maintenance Dundee, Angus and Perth.";
                    ViewBag.MetaKeywords = "RPM joinery maintenance, rpm windows, doors installation dundee, new doors dundee, dundee window fitter";
                    break;

                case "Decking/Fencing":
                    ViewBag.Title = "RPM Joinery & Maintenance | Decking and Fencing";
                    ViewBag.MetaDescription = "Complete decking and fencing installations by RPM Joinery & Maintenance Dundee, Angus and Perth.";
                    ViewBag.MetaKeywords = "RPM joinery maintenance, rpm fence, fence installation dundee, new decking dundee, dundee decking joiner";
                    break;

                case "Maintenance":
                    ViewBag.Title = "RPM Joinery & Maintenance | Small Maintenance";
                    ViewBag.MetaDescription = "Small maintenance jobs by RPM Joinery & Maintenance Dundee, Angus and Perth.";
                    ViewBag.MetaKeywords = "RPM joinery maintenance, rpm maintenance, dundee maintenance, small maintenance joiner, dundee joiner property management";
                    break;
            }

            // Switch - change SEO keywords/description based on the tags which are being searched for
            

            try
            {
                var usedID = GetCurrentUserID();
                var username = User.Identity.GetUserName();

                ViewBag.UserID = usedID;
                ViewBag.UserName = username;
            } catch(SystemException ex)
            {

            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticated = true;
            } else
            {
                ViewBag.IsAuthenticated = false;
            }

            //get TYPE 
            if(TempData["searchTag"] != null && TempData["searchTag"].ToString() != "All")
            {
                return View(db.Projects.AsEnumerable().Where(x => ContainsSearchTag(TempData["searchTag"].ToString(), x.Type) == true).ToList());
            } else
            {
                return View(db.Projects.ToList());
            }

        }

        public ActionResult Kitchens()
        {
            TempData["searchTag"] = "Kitchen";
            TempData["searchTitle"] = "Kitchens";
            return RedirectToAction("Index");
        }

        public ActionResult Bathroom()
        {
            TempData["searchTag"] = "Bathroom";
            TempData["searchTitle"] = "Bathrooms";
            return RedirectToAction("Index");
        }

        public ActionResult WindowsDoors()
        {
            TempData["searchTag"] = "Windows/Doors";
            TempData["searchTitle"] = "Windows & Doors";
            return RedirectToAction("Index");
        }

        public ActionResult DeckingFencing()
        {
            TempData["searchTag"] = "Decking/Fencing";
            TempData["searchTitle"] = "Decking & Fencing";
            return RedirectToAction("Index");
        }

        public ActionResult Maintenance()
        {
            TempData["searchTag"] = "Maintenance";
            TempData["searchTitle"] = "Maintenance";
            return RedirectToAction("Index");
        }

        public ActionResult All()
        {
            TempData["searchTag"] = "All";
            return RedirectToAction("Index");
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticated = true;
            }
            else
            {
                ViewBag.IsAuthenticated = false;
            }


            return View(project);
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            try
            {
                var usedID = GetCurrentUserID();
                ViewBag.UserID = usedID;
            }
            catch (SystemException ex)
            {

            }
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserID,Title,Description,Type,Details,ImgDescription,ImgDescription2,ImgDescription3,ImgDescription4,ImgDescription5")] Project project, string[] type, HttpPostedFileBase image, HttpPostedFileBase image2, HttpPostedFileBase image3, HttpPostedFileBase image4, HttpPostedFileBase image5)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    //Save image to file
                    Stream st = image.InputStream;
                    string filename = db.Projects.ToList().Count() + "-1";
                    string bucketName = "rpmjoinery";
                    string s3DirectoryName = "projects";
                    string s3FileName = "" + filename;
                    project.ImgFilePath = "https://s3-eu-west-1.amazonaws.com/rpmjoinery/projects/" + s3FileName;
                    bool a;
                    AmazonUploader myUploader = new AmazonUploader();
                    a = myUploader.sendMyFileToS3(st, bucketName, s3DirectoryName, s3FileName);
                    if (a == true)
                    {
                        Response.Write("successfully uploaded");
                    }
                    else
                        Response.Write("Error");
                }

                if (image2 != null)
                {
                    //Save image to file
                    Stream st = image2.InputStream;
                    string filename = db.Projects.ToList().Count() + "-2";
                    string bucketName = "rpmjoinery";
                    string s3DirectoryName = "projects";
                    string s3FileName = "" + filename;
                    project.ImgFilePath2 = "https://s3-eu-west-1.amazonaws.com/rpmjoinery/projects/" + s3FileName;
                    bool a;
                    AmazonUploader myUploader2 = new AmazonUploader();
                    a = myUploader2.sendMyFileToS3(st, bucketName, s3DirectoryName, s3FileName);
                    if (a == true)
                    {
                        Response.Write("successfully uploaded");
                    }
                    else
                        Response.Write("Error");
                }

                if (image3 != null)
                {
                    //Save image to file
                    Stream st = image3.InputStream;
                    string filename = db.Projects.ToList().Count() + "-3";
                    string bucketName = "rpmjoinery";
                    string s3DirectoryName = "projects";
                    string s3FileName = "" + filename;
                    project.ImgFilePath3 = "https://s3-eu-west-1.amazonaws.com/rpmjoinery/projects/" + s3FileName;
                    bool a;
                    AmazonUploader myUploader3 = new AmazonUploader();
                    a = myUploader3.sendMyFileToS3(st, bucketName, s3DirectoryName, s3FileName);
                    if (a == true)
                    {
                        Response.Write("successfully uploaded");
                    }
                    else
                        Response.Write("Error");
                }

                if (image4 != null)
                {
                    //Save image to file
                    Stream st = image4.InputStream;
                    string filename = db.Projects.ToList().Count() + "-4";
                    string bucketName = "rpmjoinery";
                    string s3DirectoryName = "projects";
                    string s3FileName = "" + filename;
                    project.ImgFilePath4 = "https://s3-eu-west-1.amazonaws.com/rpmjoinery/projects/" + s3FileName;
                    bool a;
                    AmazonUploader myUploader4 = new AmazonUploader();
                    a = myUploader4.sendMyFileToS3(st, bucketName, s3DirectoryName, s3FileName);
                    if (a == true)
                    {
                        Response.Write("successfully uploaded");
                    }
                    else
                        Response.Write("Error");
                }

                if (image5 != null)
                {
                    //Save image to file
                    Stream st = image5.InputStream;
                    string filename = db.Projects.ToList().Count() + "-5";
                    string bucketName = "rpmjoinery";
                    string s3DirectoryName = "projects";
                    string s3FileName = "" + filename;
                    project.ImgFilePath5 = "https://s3-eu-west-1.amazonaws.com/rpmjoinery/projects/" + s3FileName;
                    bool a;
                    AmazonUploader myUploader5 = new AmazonUploader();
                    a = myUploader5.sendMyFileToS3(st, bucketName, s3DirectoryName, s3FileName);
                    if (a == true)
                    {
                        Response.Write("successfully uploaded");
                    }
                    else
                        Response.Write("Error");
                }

                string tagString = "";
                if(type.Count() > 0)
                {
                    foreach(string tag in type)
                    {
                        tagString += " " + tag;
                    }
                } else
                {
                    project.Type = "All ";
                }
                project.Type = tagString;

                db.Projects.Add(project);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        public bool ContainsSearchTag(string tag, string tagString)
        {
            string[] strings = tagString.Split(' ');
            foreach(string s in strings)
            {
                if (s == tag)
                    return true;
            }

            return false;
        }

        // GET: Projects/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserID,Title,Description,Type,Details,ImgDescription,ImgDescription2,ImgDescription3,ImgDescription4,ImgDescription5")] Project project)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(project).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Guid GetCurrentUserID()
        {
            return new Guid(User.Identity.GetUserId());
        }
                
    }
}
