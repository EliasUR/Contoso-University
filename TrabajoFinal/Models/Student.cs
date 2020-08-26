using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabajoFinal.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required][DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public List<Course> Courses { get; set; }
    }
}