using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabajoFinal.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required][StringLength(100)]
        public string Name { get; set; }
        [Required][DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}