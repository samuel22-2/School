using MessagePack;

namespace WebApplication3.models
{
    public class Registration
    {
        
        public int Id { get; set; }
        public string ApplicantId { get; set; } = Guid.NewGuid().ToString();    
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
        public string JambScore{ get; set; }
        public string Password { get; set; } = Guid.NewGuid().ToString();
        public double TotalScore { get; set; } = 0;
        public int DepartmentId { get; set; }

    }

    public class School
    {
        public int Id { get; set; }
        public string RegId { get; set; }
        public string SchoolName { get; set; }
    }

    public class Exam
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string RegId { get; set;}
        public string Subject { get; set; }
        public string Grade { get; set; }
    }
}
