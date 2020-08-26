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
    public class DepartmentsController : Controller
    {
        private SchoolRepository repo = new SchoolRepository();


        public ActionResult Index()
        {
            List<Department> departments = (List<Department>)repo.GetList("Department", "", null);

            return View(departments);
        }
        public ActionResult Search(FormCollection form)
        {
            string title = form["Title"];

            List<Department> departments = (List<Department>)repo.GetList("Department", title, null);

            return View("Index", departments);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] Department department)
        {
            if (ModelState.IsValid)
            {
                repo.Create(department, "Department");
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int id)
        {
            Department department = (Department)repo.GetOne(id, "Department");

            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description")] Department department)
        {
            if (ModelState.IsValid)
            {
                repo.Edit(department, "Department");
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public ActionResult Delete(int id)
        {

            repo.Delete(id, "Department");

            return RedirectToAction("Index");
        }
    }
}
