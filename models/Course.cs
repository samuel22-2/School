namespace WebApplication3.models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public string Semester { get; set; }
        public int Level { get; set; }
        public Department department { get; set; }

    }
}
