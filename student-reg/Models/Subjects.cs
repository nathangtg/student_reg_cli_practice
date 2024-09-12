using System;

namespace student_reg.Models
{
    public class Subjects
    {
        public enum SubjectList
        {
            English,
            Maths,
            Science,
            Social,
            Hindi,
            French,
            German,
            Spanish,
            Japanese,
            Chinese
        }

        public string Name { get; set; }
        public string Id { get; set; }
        public Grade Grade { get; set; }
        public int Marks { get; set; }

        public Subjects(SubjectList name, string id, Grade grade)
        {
            Name = name.ToString();
            Id = id;
            Grade = grade;
        }
    }
}
