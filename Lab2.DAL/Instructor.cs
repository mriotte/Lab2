using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DAL
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Навігаційна властивість для зв'язку з Course
        public ICollection<CourseInstructor> CourseInstructors { get; set; }
    }
}
