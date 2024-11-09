using Lab2.DAL;
using Lab2.DAL.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class DatabaseInitializer
    {
        private readonly LabDbContext _context;

        public DatabaseInitializer(LabDbContext context)
        {
            _context = context;
        }

        public async Task Initialize()
        {
            // Створення тестових курсів
            if (!_context.Courses.Any())
            {
                _context.Courses.AddRange(
                    new Course { Title = "Mathematics", Credits = 5 },
                    new Course { Title = "Physics", Credits = 4 },
                    new Course { Title = "Chemistry", Credits = 4 },
                    new Course { Title = "Computer Science", Credits = 3 }
                );
                await _context.SaveChangesAsync();
            }

            // Створення тестових студентів
            if (!_context.Students.Any())
            {
                _context.Students.AddRange(
                    new Student { FirstName = "John", LastName = "Doe" },
                    new Student { FirstName = "Jane", LastName = "Smith" },
                    new Student { FirstName = "Tom", LastName = "Brown" },
                    new Student { FirstName = "Emily", LastName = "White" }
                );
                await _context.SaveChangesAsync();
            }

            // Створення зв'язків між студентами та курсами
            if (!_context.StudentCourses.Any())
            {
                var student1 = await _context.Students.FirstAsync(s => s.FirstName == "John");
                var student2 = await _context.Students.FirstAsync(s => s.FirstName == "Jane");
                var student3 = await _context.Students.FirstAsync(s => s.FirstName == "Tom");
                var student4 = await _context.Students.FirstAsync(s => s.FirstName == "Emily");

                var course1 = await _context.Courses.FirstAsync(c => c.Title == "Mathematics");
                var course2 = await _context.Courses.FirstAsync(c => c.Title == "Physics");
                var course3 = await _context.Courses.FirstAsync(c => c.Title == "Chemistry");

                _context.StudentCourses.AddRange(
                    new StudentCourse { StudentId = student1.StudentId, CourseId = course1.CourseId },
                    new StudentCourse { StudentId = student1.StudentId, CourseId = course2.CourseId },
                    new StudentCourse { StudentId = student2.StudentId, CourseId = course3.CourseId },
                    new StudentCourse { StudentId = student3.StudentId, CourseId = course1.CourseId },
                    new StudentCourse { StudentId = student4.StudentId, CourseId = course2.CourseId }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
