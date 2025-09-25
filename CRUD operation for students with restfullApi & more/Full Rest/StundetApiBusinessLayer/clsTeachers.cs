using StudentDataAccsessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentDataAccsessLayer;

namespace StundetApiBusinessLayer
{

    public class clsTeachers
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public TeacherDTO TDTO { get { return new TeacherDTO(Id, FullName, Username, PasswordHash, Email); } }

        public clsTeachers(TeacherDTO dto)
        {
            Id = dto.Id;
            FullName = dto.FullName;
            Username = dto.Username;
            PasswordHash = dto.PasswordHash;
            Email = dto.Email;
        }

        public static clsTeachers GetTeacherByUsername(string username)
        {
            var dto = TeacherData.GetTeacherByUsername(username);
            if (dto == null) return null;
            return new clsTeachers(dto);
        }

        public static int AddTeacher(TeacherDTO teacher)
        {
            return TeacherData.AddTeacher(teacher);
        }

        public bool ValidatePassword(string password)
        {
            return PasswordHash == password;
        }
    }



}
