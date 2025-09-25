using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDataAccsessLayer;
using StundetApiBusinessLayer;

namespace ApiWebLayer.Controllers
{
    [Route("api/Teachers")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        [HttpPost("Register")]
        public ActionResult Register(TeacherDTO teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.Username) || string.IsNullOrWhiteSpace(teacher.PasswordHash))
                return BadRequest("Username and password are required.");

            int id = clsTeachers.AddTeacher(teacher);
            if (id > 0)
                return Ok($"Teacher registered with ID: {id}");
            else
                return StatusCode(500, "Error registering teacher.");
        }

        [HttpPost("Login")]
        public ActionResult Login(string username, string password)
        {
            var teacher = clsTeachers.GetTeacherByUsername(username);
            if (teacher == null)
                return NotFound("Teacher not found.");

            if (!teacher.ValidatePassword(password))
                return Unauthorized("Invalid password.");

            return Ok($"Welcome {teacher.FullName}!");
        }
    }
}
