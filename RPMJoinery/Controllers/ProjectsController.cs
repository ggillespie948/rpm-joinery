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
            try{
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
            // return View(db.Projects.Where(x => x.UserId == userID).ToList());

            return View(db.Projects.ToList());
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
        public ActionResult Create([Bind(Include = "Id,UserID,Title,Description,Type,Details")] Project project, string[] type, HttpPostedFileBase image, HttpPostedFileBase image2, HttpPostedFileBase image3, HttpPostedFileBase image4, HttpPostedFileBase image5)
        {
            if (ModelState.IsValid)
            {

                if(image != null)
                {
                    //Save image to file
                    Stream st = image.InputStream;
                    string filename = db.Projects.ToList().Count() + "-1";
                    string bucketName = "rpmjoinery";
                    string s3DirectoryName = "";
                    string s3FileName = "" + filename;
                    project.ImgFilePath = "https://s3-eu-west-1.amazonaws.com/rpmjoinery/" + filename;
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

                string tagString = "";
                foreach(string tag in type)
                {
                    tagString += " " + tag;
                }
                project.Type = tagString;

                project.ImgFilePath = "";

                db.Projects.Add(project);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
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
        public ActionResult Edit([Bind(Include = "Id,UserID,Title,Description,Type,Details")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
