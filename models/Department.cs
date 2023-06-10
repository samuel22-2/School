namespace WebApplication3.models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }//comp sc
        public int FacultyId { get; set; } // 2
        public Faculty Faculty { get; set; }

        //public Faculty? Faculty { get; set; }
    }
}
