using Microsoft.Data.SqlClient;
using System.Data;

namespace StudentDataAccsessLayer
{
    public class TeacherData
    {
        private readonly static string connectionString =
            "Server=localhost;Database=StudentsDB;User Id=sa;Password=123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        public static int AddTeacher(TeacherDTO teacher)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_AddTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", teacher.FullName);
                    cmd.Parameters.AddWithValue("@Username", teacher.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", teacher.PasswordHash);
                    cmd.Parameters.AddWithValue("@Email", teacher.Email ?? (object)DBNull.Value);

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public static TeacherDTO GetTeacherByUsername(string username)
        {
            TeacherDTO teacher = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_GetTeacherByUsername", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teacher = new TeacherDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("Username")),
                                reader.GetString(reader.GetOrdinal("PasswordHash")),
                                reader["Email"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("Email"))
                            );
                        }
                    }
                }
            }
            return teacher;
        }
    }
}
