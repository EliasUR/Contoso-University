using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabajoFinal.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required][StringLength(50)]
        public string Title { get; set; }
        [Required][StringLength(200)][DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public List<Course> Courses { get; set; }
    }
}