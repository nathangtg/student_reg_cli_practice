using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace student_reg.Tests
{
    public class StudentTests
    {
        [Fact]
        public void CreateStudent_ShouldSetProperties()
        {
            // Arrange
            var name = "John Doe";
            var dob = new DateTime(2000, 1, 1);
            var id = "S001";
            var grade = new Grade(10);

            // Act
            var student = new Student(name, dob, id) { Grade = grade };

            // Assert
            Assert.Equal(name, student.Name);
            Assert.Equal(dob, student.Dob);
            Assert.Equal(id, student.Id);
            Assert.Equal(grade, student.Grade);
        }
    }
}