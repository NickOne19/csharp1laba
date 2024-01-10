using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [MaxLength(30)]
        public string? StudentName { get; set; }
        [MaxLength(30)]
        public string? StudentSurname { get; set; }
        [Required]
        public int Age { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public List<Course> Courses { get; set; } = new();

    }
}
