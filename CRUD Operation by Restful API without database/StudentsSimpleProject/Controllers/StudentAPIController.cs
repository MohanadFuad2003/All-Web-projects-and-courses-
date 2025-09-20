using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsSimpleProject.Models;
using StudentsSimpleProject.DataSimulation;
using System.Linq;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace StudentsSimpleProject.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {

        [HttpGet("GetAllStudents")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(DataSimulation.StudentDataSumilation.Students);
        }

        [HttpGet("GetAllStudentsPassed")]

        public ActionResult<IEnumerable<Student>> GetAllStudentsPassed()
        {
            var passedStudents = StudentDataSumilation.Students.Where(s => s.Grade >= 50).ToList();
            return Ok(passedStudents);
        }

        [HttpGet("GetAllStudentsFailed")]

        public ActionResult<IEnumerable<Student>> GetAllStudentsFailed()
        {
            var failedStudents = StudentDataSumilation.Students.Where(s => s.Grade < 50).ToList();
            return Ok(failedStudents);
        }


        [HttpGet("GetAverageForAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAvgGradeForAll()
        {
            if (StudentDataSumilation.Students.Count == 0)
            {
                return NotFound("No Student Here");
            }
            double getAvg = Convert.ToDouble(StudentDataSumilation.Students.Average(s => s.Grade));

            return Ok(getAvg);
        }



        [HttpGet("GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Student> GetStudentById(int id)
        {

            var student = StudentDataSumilation.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            else if (student.Id != id)
            {
                return BadRequest("Invalid ID");
            }
            return Ok(student);
        }


        [HttpPost("AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> AddNewStudent(Student NewStudent)
        {
            if (NewStudent == null || string.IsNullOrEmpty(NewStudent.Name) || NewStudent.Age <= 0 || NewStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            if (StudentDataSumilation.Students.Count == 0)
            {
                NewStudent.Id = 1; // Start IDs from 1 if the list is empty
            }
            else
            {
                NewStudent.Id = StudentDataSumilation.Students.Max(s => s.Id) + 1; // Auto-increment ID
            }

            StudentDataSumilation.Students.Add(NewStudent);
            return CreatedAtAction((string)nameof(GetStudentById), new { id = NewStudent.Id }, NewStudent);

             

        }

        [HttpDelete("DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeleteStudentById(int id)
        {
            if(id < 1)
            {
                return BadRequest($"Cannot Accept ID less than 1  , your id is : {id}");
            }
            var student = StudentDataSumilation.Students.FirstOrDefault(s => s.Id == id);
     
            if (student == null)
            {

                return NotFound($"Student with ID {id} not found.");
            }
            StudentDataSumilation.Students.Remove(student);
            return NoContent();
        }

        [HttpPut("UpdateStudentById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateStudentById(int id, Student updatedStudent)
        {
            if (id < 1) {
                return BadRequest($"Cannot Accept ID less than 1  , your id is : {id}");
            }

            if (updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age <= 0 || updatedStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            var existingStudent = StudentDataSumilation.Students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Grade = updatedStudent.Grade;
            return NoContent();
        }
    }
}