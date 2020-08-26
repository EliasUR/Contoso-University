using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabajoFinal.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required][StringLength(50)]
        public string Title { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [Required]
        public int InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; }
        [Required][Range(0,99)]
        public int Capacity { get; set; }
        public List<Student> Students { get; set; }
}
}