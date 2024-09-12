using System;
using System.Collections.Generic;
using student_reg.Models;

namespace student_reg
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display Students by Grade");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Edit Student Information");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. View Students by Subject");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option (1-7): ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddStudent(students);
                        break;
                    case "2":
                        DisplayStudentsByGrade(students);
                        break;
                    case "3":
                        DisplayAllStudents(students);
                        break;
                    case "4":
                        EditStudentInformation(students);
                        break;
                    case "5":
                        DeleteStudent(students);
                        break;
                    case "6":
                        ViewStudentsBySubject(students);
                        break;
                    case "7":
                        return; 
                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 7.");
                        break;
                }
            }
        }

        static void AddStudent(List<Student> students)
        {
            Console.WriteLine("\nEnter details for a new student:");

            string studentName = Prompt("Enter Student Name: ");
            DateTime studentDob = PromptDate("Enter Student Date of Birth (yyyy-MM-dd): ");
            string studentId = Prompt("Enter Student ID: ");
            int gradeValue = PromptInt("Enter Grade (1-12): ", 1, 12);
            Grade grade = new Grade(gradeValue);

            DisplaySubjects();

            int subjectCount = grade.getMaxSubjects();
            for (int i = 0; i < subjectCount; i++)
            {
                string subjectName = Prompt("Choose a subject from the list above (e.g., English, Maths): ");
                if (Enum.TryParse(subjectName, true, out Subjects.SubjectList chosenSubject))
                {
                    try
                    {
                        int marks = AskForMarks(chosenSubject.ToString());
                        grade.AddSubject(new Subjects(chosenSubject, $"ID{i + 1:D3}", grade) { Marks = marks });
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        i--;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid subject. Try again.");
                    i--;
                }
            }

            Student student = new Student(studentName, studentDob, studentId) { Grade = grade, Subjects = grade.Subjects };
            students.Add(student);
            Console.WriteLine("Student added successfully.");
        }

        static void DisplayStudentsByGrade(List<Student> students)
        {
            int displayGrade = PromptInt("Enter the grade to display (1-12): ", 1, 12);

            Console.WriteLine($"\nStudents in Grade {displayGrade}:");
            foreach (var student in students)
            {
                if (student.Grade.GradeValue == displayGrade)
                {
                    DisplayStudentInfo(student);
                }
            }
        }

        static void DisplayAllStudents(List<Student> students)
        {
            Console.WriteLine("\nAll Students:");
            foreach (var student in students)
            {
                DisplayStudentInfo(student);
            }
        }

        static void EditStudentInformation(List<Student> students)
        {
            string studentId = Prompt("Enter Student ID to edit: ");
            var student = students.Find(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine($"Editing information for {student.Name}:");
            string newName = Prompt("Enter new name (or press Enter to keep current): ");
            if (!string.IsNullOrEmpty(newName)) student.Name = newName;

            string dobInput = Prompt("Enter new DOB (yyyy-MM-dd) (or press Enter to keep current): ");
            if (DateTime.TryParse(dobInput, out DateTime newDob)) student.Dob = newDob;

            string gradeInput = Prompt("Enter new Grade (1-12) (or press Enter to keep current): ");
            if (int.TryParse(gradeInput, out int newGrade) && newGrade >= 1 && newGrade <= 12)
                student.Grade = new Grade(newGrade);

            Console.WriteLine("Edit complete.");
        }

        static void DeleteStudent(List<Student> students)
        {
            string studentId = Prompt("Enter Student ID to delete: ");
            var student = students.Find(s => s.Id == studentId);
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Student removed successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static void ViewStudentsBySubject(List<Student> students)
        {
            string subjectName = Prompt("Enter the subject to display (e.g., English, Maths): ");
            if (Enum.TryParse(subjectName, true, out Subjects.SubjectList chosenSubject))
            {
                Console.WriteLine($"\nStudents with {chosenSubject} subject:");
                foreach (var student in students)
                {
                    foreach (var subject in student.Subjects)
                    {
                        if (subject.Name == chosenSubject.ToString())
                        {
                            Console.WriteLine($"Student: {student.Name}, DOB: {student.Dob.ToShortDateString()}, ID: {student.Id}, Marks: {subject.Marks}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid subject.");
            }
        }

        static int AskForMarks(string subjectName)
        {
            return PromptInt($"Enter marks for {subjectName}: ", 0, 100);
        }

        static void DisplayStudentInfo(Student student)
        {
            Console.WriteLine($"Student: {student.Name}, DOB: {student.Dob.ToShortDateString()}, ID: {student.Id}, Grade: {student.Grade.GradeValue}");
            Console.WriteLine("Subjects and Marks:");
            foreach (var subject in student.Subjects)
            {
                Console.WriteLine($"- {subject.Name}: {subject.Marks} marks");
            }
            Console.WriteLine();
        }

        static string Prompt(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine()!;
        }

        static DateTime PromptDate(string message)
        {
            DateTime date;
            Console.WriteLine(message);
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Invalid date format. Please enter in yyyy-MM-dd format:");
            }
            return date;
        }

        static int PromptInt(string message, int minValue, int maxValue)
        {
            int value;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out value) || value < minValue || value > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}:");
            }
            return value;
        }

        static void DisplaySubjects()
        {
            Console.WriteLine("Available Subjects:");
            foreach (var subject in Enum.GetValues(typeof(Subjects.SubjectList)))
            {
                Console.WriteLine($"- {subject}");
            }
        }
    }
}
