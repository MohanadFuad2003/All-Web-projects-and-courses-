﻿namespace StudentDataAccsessLayer
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
        public StudentDTO(int id, string name, int age, int grade)
        {
            this.Id = id;
           this.Name = name;
            this.Age = age;
            this.Grade = grade;
        }

    }
}
