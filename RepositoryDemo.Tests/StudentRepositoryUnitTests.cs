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
        [Fact]
        public void UpdateStudent_ShouldModifyStudentDetails()
        {
            // Arrange
            _repository.Add("David", 26);
            int id = _context.Students.First().Id;

            // Act
            _repository.Update(id, "David Updated", 30);
            var updatedStudent = _context.Students.FirstOrDefault(s => s.Id == id);

            // Assert
            Assert.NotNull(updatedStudent);
            Assert.Equal("David Updated", updatedStudent.Name);
            Assert.Equal(30, updatedStudent.Age);
        }

        [Fact]
        public void DeleteStudent_ShouldRemoveStudentFromDatabase()
        {
            // Arrange
            _repository.Add("Eve", 28);
            int id = _context.Students.First().Id;

            // Act
            _repository.DeleteStudent(id);
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            // Assert
            Assert.Null(student);
        }

        [Fact]
        public void DeleteAllStudents_ShouldRemoveAllStudents()
        {
            // Arrange
            _repository.Add("Frank", 21);
            _repository.Add("Grace", 23);

            // Act
            _repository.DeleteAllStudents();
            var students = _context.Students.ToList();

            // Assert
            Assert.Empty(students);
        }


    }
}