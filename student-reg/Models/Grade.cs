using System;
using System.Collections.Generic;

namespace student_reg.Models
{
    public class Grade
    {
        private int gradeValue;
        private List<Student> students;
        private List<Subjects> subjects;

        public Grade(int gradeValue)
        {
            GradeValue = gradeValue;
            students = new List<Student>();
            subjects = new List<Subjects>();
        }

        public int GradeValue
        {
            get { return gradeValue; }
            private set { gradeValue = value; }
        }

        public List<Subjects> Subjects => subjects;
        public List<Student> Students => students;

        private int MaxSubjects
        {
            get
            {
                return gradeValue switch
                {
                    int n when (n >= 1 && n <= 3) => 3,
                    int n when (n >= 4 && n <= 6) => 5,
                    int n when (n >= 7 && n <= 9) => 7,
                    int n when (n >= 10 && n <= 12) => 10,
                    _ => throw new ArgumentOutOfRangeException(nameof(gradeValue), "Invalid grade value")
                };
            }
        }


        public void AddSubject(Subjects subject)
        {
            if (subjects.Count < MaxSubjects)
                subjects.Add(subject);
            else
                throw new InvalidOperationException($"Grade {gradeValue} can only have up to {MaxSubjects} subjects.");
        }

        public void RemoveSubject(Subjects subject) => subjects.Remove(subject);

        public void AddStudent(Student student) => students.Add(student);

        public void RemoveStudent(Student student) => students.Remove(student);

        public int getMaxSubjects()
        {
            return MaxSubjects;
        }
    }
}
