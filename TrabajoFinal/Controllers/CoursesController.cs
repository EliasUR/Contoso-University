using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabajoFinal.Dal;
using TrabajoFinal.Models;
using TrabajoFinal.Services;

namespace TrabajoFinal.Controllers
{
    public class CoursesController : Controller
    {
        private SchoolRepository repo = new SchoolRepository();

        public ActionResult Index()
        {
            List<Course> courses = (List<Course>)repo.GetList("Course", "", null);

            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);

            return View(courses);
        }
        public ActionResult Search(FormCollection form)
        {
            string title = form["Title"];
            int id = Convert.ToInt32(form["Department"]);

            List<Course> courses = (List<Course>)repo.GetList("Course", title, id);

            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);

            return View("Index", courses);
        }

        // GET: Courses/CourseStudents/5
        public ActionResult CourseStudents(int id)
        {
            Course course = (Course)repo.GetOne(id, "CourseStudents");

            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);
            ViewBag.Instructors = (List<Instructor>)repo.GetList("Instructor", "", null);

            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DepartmentId,InstructorId,Capacity")] Course course)
        {
            if (ModelState.IsValid)
            {
                repo.Create(course, "Course");
                return RedirectToAction("Index");
            }
            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);
            ViewBag.Instructors = (List<Instructor>)repo.GetList("Instructor", "", null);

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);
            ViewBag.Instructors = (List<Instructor>)repo.GetList("Instructor", "", null);

            Course course = (Course)repo.GetOne(id, "Course");

            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,DepartmentId,InstructorId,Capacity")] Course course)
        {
            if (ModelState.IsValid)
            {
                repo.Edit(course, "Course");
                return RedirectToAction("Index");
            }
            ViewBag.Departments = (List<Department>)repo.GetList("Department", "", null);
            ViewBag.Instructors = (List<Instructor>)repo.GetList("Instructor", "", null);

            return View(course);
        }

        public ActionResult Delete(int id)
        {
            repo.Delete(id, "Course");

            return RedirectToAction("Index");
        }
    }
}
