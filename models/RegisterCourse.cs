namespace WebApplication3.models
{
    public class RegisterCourse
    {
        public int Id { get; set; } 
        public int MatricNo { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public int LevelId { get; set; }  
        public string DepartmentId { get; set; }
        public string FacultyId { get; set; }
        public string SessionId { get; set; }
        public DateTime  RegisteredDate { get; set; }
    }
}
