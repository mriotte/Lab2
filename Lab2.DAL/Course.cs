using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DAL
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; } // Нове поле

        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<CourseInstructor> CourseInstructors { get; set; }
    }
}
