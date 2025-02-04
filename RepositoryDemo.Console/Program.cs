// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Domain;
using System;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

SQLitePCL.Batteries_V2.Init();

var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseSqlite("Data Source=c:/sqlite/students-books-2025-02-04.db")
.Options;

using var context = new AppDbContext(options);
var _studentRepository = new StudentRepository(context);



// Seed Data
var library = new Library { Name = "City Library" };
var author1 = new Author { Name = "Author One" };
var author2 = new Author { Name = "Author Two" };

var book1 = new Book { Title = "Book A", Library = library, Authors = new List<Author> { author1 } };
var book2 = new Book { Title = "Book B", Library = library, Authors = new List<Author> { author1, author2 } };

context.Libraries.Add(library);
context.Authors.AddRange(author1, author2);
context.Books.AddRange(book1, book2);
context.SaveChanges();


// Query Data
var libraries = context.Libraries.Include(l => l.Books).ThenInclude(b => b.Authors).ToList();
foreach (var lib in libraries)
{
    Console.WriteLine($"Library: {lib.Name}");
    foreach (var book in lib.Books)
    {
        Console.WriteLine($"  Book: {book.Title}");
        Console.WriteLine($"    Authors: {string.Join(", ", book.Authors.Select(a => a.Name))}");
    }
}

return;

//var student1 = new Student { Name = "Bruce", Age = 45 };
//var student2 = new Student { Name = "Michael", Age =54 };
//repo.Add(student1);
//repo.Add(student2);
//repo.Add( "John", 22);
//repo.Add("Jane", 23);



//var students = repo.GetAll();

//foreach(var student in students)
//{
//    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
//}   

Console.WriteLine("SQLite Student Management System");
while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. View All Students");
    Console.WriteLine("3. Find Student by ID");
    Console.WriteLine("4. Update Student");
    Console.WriteLine("5. Delete Student");
    Console.WriteLine("6. Delete All Students");
    Console.WriteLine("7. Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine()!;
    switch (choice)
    {
        case "1":
            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;
            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine()!);
            Student newStudent = new Student { Name = name, Age = age };
            _studentRepository.Add(name, age);

            break;

        case "2":
            var students = _studentRepository.GetAll();
            Console.WriteLine("\n========================");
            Console.WriteLine("\nList of Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
            Console.WriteLine("\n========================");
            break;

        case "3":
            Console.Write("Enter Student ID: ");
            int findId = int.Parse(Console.ReadLine()!);
            var foundStudent = _studentRepository.GetStudentById(findId);
            if (foundStudent != null)
            {
                Console.WriteLine($"Found: {foundStudent.Name}, Age: {foundStudent.Age}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            break;

        case "4":
            Console.Write("Enter Student ID to update: ");
            int updateId = int.Parse(Console.ReadLine()!);
            Console.Write("Enter New Name: ");
            string newName = Console.ReadLine()!;
            Console.Write("Enter New Age: ");
            int newAge = int.Parse(Console.ReadLine()!);
            _studentRepository.Update(updateId, newName, newAge);
            break;

        case "5":
            Console.Write("Enter Student ID to delete: ");
            int deleteId = int.Parse(Console.ReadLine()!);
            _studentRepository.DeleteStudent(deleteId);
            break;

        case "6":
            Console.Write("Are you sure? (yes/no): ");
            if (Console.ReadLine()?.ToLower() == "yes")
            {
                _studentRepository.DeleteAllStudents();
            }
            break;

        case "7":
            Console.WriteLine("Exiting program...");
            return;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

}