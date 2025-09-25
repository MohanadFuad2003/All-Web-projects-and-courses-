using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDataAccsessLayer;
using StundetApiBusinessLayer;

namespace ApiWebLayer.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet("GetAllStudents")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            List<StudentDTO> students = clsStudents.GetAllStudents();
            if (students != null && students.Count > 0)
            {
                return Ok(students);
            }
            else
            {
                return NotFound("No students found.");
            }
        }


        [HttpGet("GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            List<StudentDTO> HowStudentsPassed = clsStudents.GetPassedStudents();
            if (HowStudentsPassed != null && HowStudentsPassed.Count > 0)
            {
                return Ok(HowStudentsPassed);
            }
            else
            {
                return NotFound("No passed students found.");
            }
        }

        [HttpGet("GetAverageGrade")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<double> GetAverageGrade()
        {
            double averageGrade = clsStudents.GetAverageGrade();
            if (averageGrade < 0)
                return BadRequest("Error calculating average grade.");

            return Ok(averageGrade);
        }

        [HttpGet("GetStudentsWithMaxGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudentsWithMaxGrade()
        {
            List<StudentDTO> maxGradeStudents = clsStudents.GetStudentsWithMaxGrade();
            if (maxGradeStudents != null && maxGradeStudents.Count > 0)
            {
                return Ok(maxGradeStudents);
            }
            else
            {
                return NotFound("No students with maximum grade found.");
            }
        }

        [HttpGet("GetStudentById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            clsStudents student = clsStudents.GetStudentById(id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            if (id != student.Id)
            {
                return BadRequest("ID mismatch.");
            }

            StudentDTO SDTO = student.SDTO;
            return Ok(SDTO);
        }




        [HttpPost("AddStudents")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> AddStudents(StudentDTO newStudent)
        {
            if (string.IsNullOrWhiteSpace(newStudent.Name))
                return BadRequest("Name is required.");

            if (newStudent.Age <= 0 || newStudent.Age > 100)
                return BadRequest("Age must be between 1 and 100.");

            if (newStudent.Grade < 0 || newStudent.Grade > 100)
                return BadRequest("Grade must be between 0 and 100.");

            try
            {
                var student = new clsStudents(newStudent);

                student.Save();

                return CreatedAtAction( nameof(GetStudentById),  new { id = student.Id},  student.SDTO );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the student: {ex.Message}");
            }
        }

        [HttpPut("DeleteStudent")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student ID.");
            try
            {
                var student = clsStudents.GetStudentById(id);
                if (student == null)
                    return NotFound($"Student with ID {id} not found.");
                bool isDeleted = clsStudents.DeleteStudent(id);
                if (isDeleted)
                    return Ok($"Student with ID {id} has been deleted.");
                else
                    return StatusCode(500, "An error occurred while deleting the student.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("UpdateStudent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
            
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO updatedStudent)
        {
            if(UpdateStudent == null  )
            {
                return BadRequest("Student data is required.");
            }

            if (id != updatedStudent.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if(id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }   

           
            var existingStudent = clsStudents.GetStudentById(id);   
            if(existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            existingStudent.Name = updatedStudent.Name; 
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Grade = updatedStudent.Grade;
            existingStudent.Save(); 

            return Ok(existingStudent.SDTO);

        }

    }
}
