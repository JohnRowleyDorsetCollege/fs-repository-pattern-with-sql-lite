// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Domain;
using System;

Console.WriteLine("Hello, World!");

SQLitePCL.Batteries_V2.Init();

var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseSqlite("Data Source=c:/sqlite/students2025-02-04.db")
.Options;

