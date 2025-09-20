using StudentsSimpleProject.Models;

namespace StudentsSimpleProject.DataSimulation
{
    public class StudentDataSumilation
    {
        public static readonly List<Student> Students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 20, Grade = 90 },
            new Student { Id = 2, Name = "Bob", Age = 22, Grade = 85 },
            new Student { Id = 3, Name = "Charlie", Age = 21, Grade = 30 },
            new Student { Id = 4, Name = "David", Age = 23, Grade = 20 },
            new Student { Id = 5, Name = "Eve", Age = 20, Grade = 95 }
        };

    }
}
