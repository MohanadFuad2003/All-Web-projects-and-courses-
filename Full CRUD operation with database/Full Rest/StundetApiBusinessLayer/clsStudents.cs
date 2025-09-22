using StudentDataAccsessLayer;
using System.Runtime.CompilerServices;

namespace StundetApiBusinessLayer
{
    public class clsStudents
    {

        public enum enMode { AddNew = 0 , Update = 1}
        public enMode Mode = enMode.AddNew;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }


        public StudentDTO SDTO { get  { return new StudentDTO(Id, Name, Age, Grade); } }  
        
 
        public clsStudents(StudentDTO SDTO , enMode Mode = enMode.AddNew)
        {
            Id = SDTO.Id; Name = SDTO.Name; Age = SDTO.Age; Grade = SDTO.Grade;
            this.Mode = Mode;

        }


        public static List<StudentDTO> GetAllStudents()
        {
            return StudentData.GetAllStudents();    
        }

        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentData.GetPassedStudents(); 
        }

        public static double GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }   

        public static List<StudentDTO>GetStudentsWithMaxGrade()
        {
            return StudentData.GetMaxGradeStudents();
        }   

        public static clsStudents GetStudentById(int id)
        {
            var studentDTO = StudentData.GetStudentById(id);
            if (studentDTO != null)
                return new clsStudents(studentDTO, enMode.Update);
            else
                return null;

        }   

        
        public static bool DeleteStudent(int id)
        {
            return StudentData.DeleteStudent(id);
        }





        public bool Save()
        {
           switch(Mode)
            {
                case enMode.AddNew:
                    int newId = StudentData.AddNewStudent(SDTO);
                    if (newId > 0)
                    {
                        Id = newId;
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return StudentData.UpdateStudent(SDTO);
                default:
                    return false;
            }
        }





    }
}
