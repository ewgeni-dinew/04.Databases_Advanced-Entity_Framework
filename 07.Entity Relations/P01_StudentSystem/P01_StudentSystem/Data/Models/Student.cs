using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
            HomeworkSubmissions = new List<Homework>();
            CourseEnrollments = new List<StudentCourse>();
        }

        public int StudentId { get; set; }
        public string Name { get; set; }

        [MaxLength(50), MinLength(2)]
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? Birthday { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }
        public ICollection<StudentCourse> CourseEnrollments { get; set; }
    }
}
