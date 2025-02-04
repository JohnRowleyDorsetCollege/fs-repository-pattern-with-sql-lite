using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDemo.Domain
{
    public class StudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void Add(string name, int age)
        {
            var student = new Student {
                Name = name,
                Age = age
            };  
            _context.Students.Add(student);
            _context.SaveChanges();
            Console.WriteLine($"Added: {student.Name}, Age: {student.Age}");
        }

        public void Add(Student student)
        {
           
            _context.Students.Add(student);
            _context.SaveChanges();
            Console.WriteLine($"Added: {student.Name}, Age: {student.Age}");
        }

        public Student? GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(s=>s.Id==id);
        }   

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            Console.WriteLine($"Updated: {student.Name}, Age: {student.Age}");
        }

        public void Update(int id, string newName, int newAge)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student is not null)
            {
                student.Name = newName;
                student.Age = newAge;
              
                _context.SaveChanges();
                Console.WriteLine($"Updated: {student.Name}, Age: {student.Age}");
            } else
            {
                Console.WriteLine("Student not found");
            }
         
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                Console.WriteLine("Student deleted successfully");
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }

        public void DeleteAllStudents()
        {
            _context.Students.RemoveRange(_context.Students);
            _context.SaveChanges();
            Console.WriteLine("All students deleted successfully");
        }

    }
}
