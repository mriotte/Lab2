using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DAL
{
    public class CourseInstructor
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
