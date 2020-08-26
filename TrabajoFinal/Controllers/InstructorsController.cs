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
    public class InstructorsController : Controller
    {        
        private SchoolRepository repo = new SchoolRepository();

        // GET: Instructors
        public ActionResult Index()
        {
            List<Instructor> instructors = (List<Instructor>)repo.GetList("Instructor", "", null);

            return View(instructors);
        }
        public ActionResult Search(FormCollection form)
        {
            string name = form["Name"];

            List<Instructor> instructors = (List<Instructor>)repo.GetList("Instructor", name, null);

            return View("Index", instructors);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                repo.Create(instructor, "Instructor");
                return RedirectToAction("Index");
            }

            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int id)
        {
            Instructor instructor = (Instructor)repo.GetOne(id, "Instructor");

            return View(instructor);
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                repo.Edit(instructor, "Instructor");
                return RedirectToAction("Index");
            }
            return View(instructor);
        }
        public ActionResult Delete(int id)
        {   
            repo.Delete(id, "Instructor");

            return RedirectToAction("Index");
        }
    }
}
