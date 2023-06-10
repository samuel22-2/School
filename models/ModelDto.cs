namespace WebApplication3.models
{
    public class RegistrationDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string StateOfOrigin { get; set; }
        public string Denomination { get; set; }
        public string GuardianName { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public string GuardianEmail { get; set; } 
        public string GuardianAddress { get; set; }
        public string JambScore { get; set; }
        //public string Password { get; set; }
        public SchoolDTO SchoolDTO { get; set; }    
        public List<ExamDTO> ExamList { get; set; }  
        public int DepartmentId { get; set; }   



       

    }

   

    public class SchoolDTO
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }

    public class ExamDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
    }

    public class CourseDTO
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public string Semester { get; set; }
        public int Level { get; set; }
        // public Department department { get; set; }
    }

    public class DepartmentDTO
    {
        //public int Id { get; set; }
        public string? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int FacultyId { get; set; }
       // public Faculty faculty { get; set; }

    }

    public class FacultyDTO
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
      
    }
    
    public class HostelDTO
    {
        public string HostelName { get; set; }
    }

    public class WorshipCenterDTO
    {
        public string WorshipCenterName { get; set; }
    }

    public class RegisterCourseDTO
    {
        
        public int MatricNo { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public int LevelId { get; set; }
        public string DepartmentId { get; set; }
        public string FacultyId { get; set; }
        public string SessionId { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
