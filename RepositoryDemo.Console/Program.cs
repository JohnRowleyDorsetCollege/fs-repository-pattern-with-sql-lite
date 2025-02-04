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

//var student1= new Student { Name = "John", Age = 22 };  
//var student2 = new Student { Name = "Jane", Age = 23 };

repo.Add( "John", 22);
repo.Add("Jane", 23);

