using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabajoFinal.Models;
using TrabajoFinal.Services;

namespace TrabajoFinal.Controllers
{
    public class EnrollmentsController : Controller
    {
        private SchoolRepository repo = new SchoolRepository();

        // GET: Enrollments
        public ActionResult Index()
        {
            List<Course> courses = (List<Course>)repo.GetList("Enrollment", "", null);

            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);

            bool? enrolled = (bool?)Session["Enrolled"];
            ViewBag.Enrolled = enrolled;

            Session["Enrolled"] = null;

            return View(courses);
        }
        public ActionResult Search(FormCollection form)
        {
            string title = form["Title"];
            int id = Convert.ToInt32(form["Department"]);

            List<Course> courses = (List<Course>)repo.GetList("Enrollment", title, id);

            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);

            return View("Index", courses);
        }

        [HttpPost]
        public ActionResult Enroll(FormCollection form)
        {
            var student = new Student();
            student.FullName = form["FullName"];
            student.Email = form["Email"];
            int courseId = Convert.ToInt32(form["Id"]);

            Session["Enrolled"] = repo.Enroll(courseId, student);

            return RedirectToAction("Index");
        }
    }
}
