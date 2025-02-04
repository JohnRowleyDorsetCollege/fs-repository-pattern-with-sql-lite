using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Domain;

namespace RepositoryDemo.Tests
{
    public class StudentRepositoryUnitTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly StudentRepository _repository;


        public StudentRepositoryUnitTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
         .UseInMemoryDatabase(databaseName: "TestDatabase")
         .Options;

            _context = new AppDbContext(options);
            _repository = new StudentRepository(_context);

        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Clean up after each test
            _context.Dispose();
        }

        [Fact]
        public void AddStudent_ShouldAddStudentToDatabase()
        {
            _repository.Add("John Doe", 25);

            var student = _context.Students.FirstOrDefault();
            Assert.NotNull(student);
            Assert.Equal("John Doe", student.Name);
            Assert.Equal(25, student.Age);
        }

        [Fact]
        public void GetStudentById_ShouldReturnStudent()
        {
            _repository.Add("John Doe", 25);
            var student = _repository.GetStudentById(1);
            Assert.NotNull(student);
            Assert.Equal("John Doe", student.Name);
            Assert.Equal(25, student.Age);
        }

       
    }
}