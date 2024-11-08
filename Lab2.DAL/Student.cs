using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DAL
{
    public class Student
    {

        public int StudentId { get; set; } // Для SQL

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birth {  get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }



}
