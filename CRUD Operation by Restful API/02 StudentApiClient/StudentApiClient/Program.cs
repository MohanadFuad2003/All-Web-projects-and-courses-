
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace StudentApiClient
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7198/api/Students/"); // Set this to the correct URI for your API
            Console.WriteLine("Student API Client\n");
            
            await UpdateStudent(1, new Student { Id = 1, Name = "werak ferak", Age = 21, Grade = 90.0 });
        }

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching all students...\n");
                var students = await httpClient.GetFromJsonAsync<List<Student>>("GetAllStudents");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.WriteLine("\n_____________________________");
        }

        static async Task GetAllStudentsPassed()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                var students = await httpClient.GetFromJsonAsync<List<Student>>("GetAllStudentsPassed");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                    }
                }
                Console.WriteLine("\n______________________________\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


        }

        static async Task GetAllStudentsFaild()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                var students = await httpClient.GetFromJsonAsync<List<Student>>("GetAllStudentsFailed");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                    }
                }
                Console.WriteLine("\n______________________________...\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        static async Task GetAverageGrade()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                var averageGrade = await httpClient.GetFromJsonAsync<float>("GetAverageForAll");
                Console.WriteLine($"Average Grade: {averageGrade}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task GetStudentById(int id)
        {
            try
            {
                var student = await httpClient.GetFromJsonAsync<Student>($"GetStudentById?id={id}");
                if (student != null)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                }
                else
                {
                    Console.WriteLine($"Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task AddNewStudent(Student newStudent)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("AddNewStudent", newStudent);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Student added successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to add student. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task DeleteStudent(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"DeleteStudentById?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Student deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete student. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task UpdateStudent(int id, Student updatedStudent)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"UpdateStudentById?id={id}", updatedStudent);
             
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to update student. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public double Grade { get; set; }
        }
    }
}


