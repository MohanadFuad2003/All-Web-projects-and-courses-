using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccsessLayer
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public TeacherDTO(int id, string fullName, string username, string passwordHash, string email)
        {
            Id = id;
            FullName = fullName;
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }
    }
}
