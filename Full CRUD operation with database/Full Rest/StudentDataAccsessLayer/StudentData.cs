

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccsessLayer
{
    public class StudentData
    {

        private readonly static string connectionString = 
            "Server=localhost;Database=StudentsDB;User Id=sa;Password=123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        public static List<StudentDTO> GetAllStudents()
        {
            var StudentsList = new List<StudentDTO>();  
            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SP_GetAllStudents", conn))
                {
                  command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentsList.Add(new StudentDTO(
                               reader.GetInt32(reader.GetOrdinal("Id")),
                               reader.GetString(reader.GetOrdinal("Name")),
                                 reader.GetInt32(reader.GetOrdinal("Age")),
                                    reader.GetInt32(reader.GetOrdinal("Grade"))
                                ));
                        }

                    }
            
                }
            }
            return StudentsList;    
        }
        public static List<StudentDTO> GetPassedStudents()
        {
            var PassedStudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_GetPassedStudents", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PassedStudentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }

            return PassedStudentsList;
        }


        public static double GetAverageGrade()
        {
            double average = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAverageGrade", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    object result = command.ExecuteScalar(); 

                    if (result != null && result != DBNull.Value)
                    {
                        average = Convert.ToDouble(result);
                    }
                }
            }

            return average;
        }


        public static List<StudentDTO> GetMaxGradeStudents()
        {
            var allStudents = GetAllStudents();

            if (allStudents.Count == 0)
                return null;

            int maxGrade = allStudents.Max(s => s.Grade);

            var maxGradeStudents = allStudents
                .Where(s => s.Grade == maxGrade)
                .ToList();

            return maxGradeStudents;
        }


        public static StudentDTO GetStudentById(int studentId)
        {
            StudentDTO student = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_GetStudentById", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            );
                        }
                    }
                }
            }

            return student;
        }

     
        public static int AddNewStudent(StudentDTO student)
        {
            int newStudentId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_AddStudent", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Grade", student.Grade);

                    SqlParameter outputIdParam = new SqlParameter("@NewStudentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    newStudentId = (int)command.Parameters["@NewStudentId"].Value;
                }
            }

            return newStudentId;
        }

        public static bool UpdateStudent(StudentDTO student)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_UpdateStudent", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StudentId", student.Id);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Grade", student.Grade);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected > 0;
        }


        public static bool DeleteStudent(int studentId)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SP_DeleteStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowsAffected = reader.GetInt32(0); // القيمة من SELECT
                        }
                    }
                }
            }
            return rowsAffected > 0;
        }






    }
}
