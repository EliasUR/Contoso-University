using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using TrabajoFinal.Dal;
using TrabajoFinal.Models;

namespace TrabajoFinal.Services
{
    public class SchoolRepository
    {
        
        public object GetList(string ListType, string wordToSearch, int? id)
        {
            using(var db = new SchoolContext())
            {
                object list = null;
                switch (ListType)
                {
                    case "Department":
                        list = db.Departments.Where(d => d.Title.Contains(wordToSearch)).ToList();
                        break;
                    case "Instructor":
                        list = db.Instructors.Where(d => d.Name.Contains(wordToSearch)).ToList();
                        break;
                    case "Course":
                        if(id > 0 && id != null)
                        {
                            list = db.Courses.Include(c => c.Department).Include(c => c.Instructor)
                                .Where(d => d.Title.Contains(wordToSearch) && d.DepartmentId == id).ToList();
                        }
                        else
                        {
                            list = db.Courses.Include(c => c.Department).Include(c => c.Instructor)
                                .Where(d => d.Title.Contains(wordToSearch)).ToList();
                        }
                        break;
                    case "Enrollment":
                        if (id > 0 && id != null)
                        {
                            list = db.Courses.Include(c => c.Department).Include(c => c.Instructor).Include(c => c.Students)
                                .Where(d => d.Title.Contains(wordToSearch) && d.DepartmentId == id).ToList();
                        }
                        else
                        {
                            list = db.Courses.Include(c => c.Department).Include(c => c.Instructor).Include(c => c.Students)
                                .Where(d => d.Title.Contains(wordToSearch)).ToList();
                        }
                        break;
                }
                return list;
            }
        }

        public object GetOne(int id, string objectType)
        {
            using (var db = new SchoolContext())
            {
                switch (objectType)
                {
                    case "Department":
                        return db.Departments.Find(id);
                    case "Instructor":
                        return db.Instructors.Find(id);
                    case "Course":
                        return db.Courses.Include(c => c.Department).Include(c => c.Instructor)
                            .Include(c => c.Students).Where(d => d.Id == id).Single();
                    case "CourseStudents":
                        return db.Courses.Include(c => c.Department).Include(c => c.Students).Where(d => d.Id == id).Single();
                    default:
                        return null;
                }
            }
        }
        public void Create(object model, string objectType)
        {
            using (var db = new SchoolContext())
            {
                switch (objectType)
                {
                    case "Department":
                        Department newDepartment = (Department)model;
                        db.Departments.Add(newDepartment);
                        db.SaveChanges();
                        break;
                    case "Instructor":
                        Instructor newInstructor = (Instructor)model;
                        db.Instructors.Add(newInstructor);
                        db.SaveChanges();
                        break;
                    case "Course":
                        Course newCourse = (Course)model;
                        db.Courses.Add(newCourse);
                        db.SaveChanges();
                        break;
                }
            }
        }
        public bool Enroll(int courseId, Student student)
        {
            using(var db = new SchoolContext())
            {
                var course = db.Courses.Include(c => c.Students).Where(c => c.Id == courseId).FirstOrDefault();
                if(course != null && !course.Students.Any(s => s.Email == student.Email)) 
                {                  
                    if(db.Students.Any(s => s.Email == student.Email))
                    {
                        student = db.Students.Where(s => s.Email == student.Email).Single();
                    }
                    course.Students.Add(student);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void Edit(object model, string objectType)
        {
            using (var db = new SchoolContext())
            {
                switch (objectType)
                {
                    case "Department":
                        Department department = (Department)model;
                        db.Entry(department).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    case "Instructor":
                        Instructor instructor = (Instructor)model;
                        db.Entry(instructor).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    case "Course":
                        Course course = (Course)model;
                        db.Entry(course).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                }
            }
        }

        public void Delete(int id, string objectType)
        {
            using (var db = new SchoolContext())
            {
                switch (objectType)
                {
                    case "Department":
                            var department = db.Departments.Find(id);
                            db.Departments.Remove(department);
                            db.SaveChanges();
                            break;
                    case "Instructor":
                        if(!db.Courses.Include(c => c.Students).Where(c => c.InstructorId == id).Any(c => c.Students.Count > 0))
                        {
                            var instructor = db.Instructors.Find(id);
                            db.Instructors.Remove(instructor);
                            db.SaveChanges();
                        }
                        break;
                    case "Course":
                        var course = db.Courses.Find(id);
                        db.Courses.Remove(course);
                        db.SaveChanges();
                        break;
                }
            }
        }
    }
}