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

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }


    }
}
