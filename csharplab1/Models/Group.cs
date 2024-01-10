using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }


        public List<Student> Students { get; set; } = new();

        public Direction? Direction { get; set; }


    }
}