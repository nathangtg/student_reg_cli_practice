using System;
using System.Collections.Generic;

namespace student_reg.Models
{
    public class Student
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Id { get; set; }
        public Grade Grade { get; set; }
        public List<Subjects> Subjects { get; set; } = new List<Subjects>();

        public Student(string name, DateTime dob, string id)
        {
            Name = name;
            Dob = dob;
            Id = id;
            Grade = null!; 
        }
    }
}
