// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Domain;
using System;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

SQLitePCL.Batteries_V2.Init();

var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseSqlite("Data Source=c:/sqlite/students2025-02-04.db")
.Options;

using var context = new AppDbContext(options);
var repo = new StudentRepository(context);

var student1 = new Student { Name = "Bruce", Age = 45 };
var student2 = new Student { Name = "Michael", Age =54 };
repo.Add(student1);
repo.Add(student2);
repo.Add( "John", 22);
repo.Add("Jane", 23);



var students = repo.GetAll();

foreach(var student in students)
{
    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
}   