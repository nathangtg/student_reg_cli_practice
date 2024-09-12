using Xunit;
using student_reg.Models;
using System;
using System.Collections.Generic;

namespace student_reg.Tests
{
    public class GradeTests
    {
        [Fact]
        public void TestAddSubject()
        {
            // Arrange
            var grade = new Grade(10);
            var subject = new Subjects(Subjects.SubjectList.English, "ENG001", grade);

            // Act
            grade.AddSubject(subject);

            // Assert
            Assert.Contains(subject, grade.Subjects);
        }

        [Fact]
        public void TestAddSubject_ThrowsExceptionWhenMaxSubjectsReached()
        {
            // Arrange
            var grade = new Grade(10);
            for (int i = 0; i < grade.getMaxSubjects(); i++)
            {
                grade.AddSubject(new Subjects((Subjects.SubjectList)i, $"ID{i+1:D3}", grade));
            }

            var additionalSubject = new Subjects(Subjects.SubjectList.French, "FRE001", grade);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => grade.AddSubject(additionalSubject));
            Assert.Equal($"Grade {grade.GradeValue} can only have up to {grade.getMaxSubjects()} subjects.", exception.Message);
        }

        [Fact]
        public void TestRemoveSubject()
        {
            // Arrange
            var grade = new Grade(10);
            var subject = new Subjects(Subjects.SubjectList.Science, "SCI001", grade);
            grade.AddSubject(subject);

            // Act
            grade.RemoveSubject(subject);

            // Assert
            Assert.DoesNotContain(subject, grade.Subjects);
        }

        [Fact]
        public void TestMaxSubjectsBasedOnGrade()
        {
            // Arrange & Act
            var grade1 = new Grade(3);
            var grade4 = new Grade(6);
            var grade7 = new Grade(9);
            var grade10 = new Grade(12);

            // Assert
            Assert.Equal(3, grade1.getMaxSubjects());
            Assert.Equal(5, grade4.getMaxSubjects());
            Assert.Equal(7, grade7.getMaxSubjects());
            Assert.Equal(10, grade10.getMaxSubjects());
        }

        [Fact]
        public void TestAddStudent()
        {
            // Arrange
            var grade = new Grade(10);
            var student = new Student("Jane Doe", new DateTime(2002, 5, 15), "67890");

            // Act
            grade.AddStudent(student);

            // Assert
            Assert.Contains(student, grade.Students);
        }

        [Fact]
        public void TestRemoveStudent()
        {
            // Arrange
            var grade = new Grade(10);
            var student = new Student("Jane Doe", new DateTime(2002, 5, 15), "67890");
            grade.AddStudent(student);

            // Act
            grade.RemoveStudent(student);

            // Assert
            Assert.DoesNotContain(student, grade.Students);
       
        }
    }
}