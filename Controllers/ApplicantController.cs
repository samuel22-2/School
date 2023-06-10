using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using NuGet.Packaging;
using WebApplication3.models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly AdminContext _context;

        public ApplicantController(AdminContext context)
        {
            _context = context;
        }

        // GET: api/Registration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            if (_context.Registrations == null)
            {
                return NotFound();
            }
            return await _context.Registrations.ToListAsync();
        }

        // GET: api/Registration/5
        [HttpGet("GetApplicants{id}")]
        public async Task<ActionResult<Registration>> GetRegistrationById(int id)
        {
            //if (_context.Registrations == null)
            //{
            //    return NotFound();
            //}
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        [HttpGet("GetApplicants{ApplicantId}")]
        public async Task<ActionResult<Registration>> GetRegistrationByApplicantId(string applicantId)
        {
            //if (_context.Registrations == null)
            //{
            //    return NotFound();
            //}
            var registration = _context.Registrations.FirstOrDefault(x => x.ApplicantId == applicantId);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        [HttpGet("GetAdmittedStudents{MatricNo}")]
        public async Task<ActionResult<AdmittedStudent>> GetAdmittedstu(string? MatricNo)
        {
            //if (_context.Registrations == null)
            //{
            //    return NotFound();
            //}
            var admittedStudent = _context.AdmittedStudents.FirstOrDefault(x => x.MatricNo == MatricNo);

            if (admittedStudent == null)
            {
                return NotFound();
            }

            return admittedStudent;
        }

        [HttpGet("GetAllAdmittedStudents")]
        public async Task<ActionResult<IEnumerable<AdmittedStudent>>> GetAllAdmittedStudents()
        {
            if (_context.AdmittedStudents == null)
            {
                return NotFound();
            }
            return await _context.AdmittedStudents.ToListAsync();
        }


        // PUT: api/Registration/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRegistration(int id, Registration registration)
        //{
        //    if (id != registration.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(registration).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RegistrationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Registration
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        //public PassMark GetPassmark()
        //{
        //    return passmark;
        //}

        [HttpPost("Register")]
        public async Task<IActionResult> PostRegistration([FromBody] RegistrationDTO register)
        {


            var reg = new Registration()
            {
                FirstName = register.FirstName,
                MiddleName = register.MiddleName,
                LastName = register.LastName,
                PhoneNumber = register.PhoneNumber,
                Email = register.Email,
                DOB = register.DOB,
                Gender = register.Gender,
                MaritalStatus = register.MaritalStatus,
                Nationality = register.Nationality,
                StateOfOrigin = register.StateOfOrigin,
                Denomination = register.Denomination,
                Address = register.Address,
                GuardianName = register.GuardianName,
                GuardianEmail = register.GuardianEmail,
                GuardianPhoneNumber = register.GuardianPhoneNumber,
                GuardianAddress = register.GuardianAddress,
                JambScore = register.JambScore,
                DepartmentId = register.DepartmentId



            };

            _context.Registrations.Add(reg); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                var response = _context.Registrations.FirstOrDefault(x => x.Id == reg.Id);

                var sch = new School
                {
                    RegId = response.ApplicantId,
                    SchoolName = register.SchoolDTO.SchoolName,

                };

                _context.Schools.Add(sch);
                _context.SaveChanges();
                double sum = 0;
                var exams = new List<Exam>();
                if (register.ExamList.Count > 0)
                {
                    foreach (var eachexam in register.ExamList)
                    {
                        exams.Add(new Exam
                        {
                            RegId = response.ApplicantId,
                            Subject = eachexam.Subject,
                            Grade = eachexam.Grade,


                        });

                        sum += Admission.SendScore(eachexam.Grade);



                    }

                    _context.Exams.AddRange(exams);
                    _context.SaveChanges();
                }





                var percent = (sum / 25 * 35) + (Convert.ToDouble(register.JambScore) / 400 * 65);
                var cutoff = PassMark.CutOff(register.DepartmentId);

                reg.TotalScore = percent;
                _context.Registrations.Update(reg);
                _context.SaveChanges();

                if (percent >= cutoff)
                {
                    var subject = "Applicant Admissiion!";
                    var body = $" Dear {register.FirstName} {register.LastName}, Thank You For Registering. You have been given provisional Admission into CyberUniversity. Your Total Score is {percent} and  Your default password is {response.Password}. You can login with your email and default passsword to change your password and register for the Semester. Thank You   ";
                    Email.SendEmail(register.Email, register.FirstName, subject, body);

                    var adm = new AdmittedStudent()
                    {
                        ApplicantId = reg.ApplicantId,
                        FirstName = register.FirstName,
                        MiddleName = register.MiddleName,
                        LastName = register.LastName,
                        PhoneNumber = register.PhoneNumber,
                        Email = register.Email,
                        DOB = register.DOB,
                        Gender = register.Gender,
                        MaritalStatus = register.MaritalStatus,
                        Nationality = register.Nationality,
                        StateOfOrigin = register.StateOfOrigin,
                        Denomination = register.Denomination,
                        Address = register.Address,
                        GuardianName = register.GuardianName,
                        GuardianEmail = register.GuardianEmail,
                        GuardianPhoneNumber = register.GuardianPhoneNumber,
                        GuardianAddress = register.GuardianAddress,
                        Password = reg.Password,
                        MatricNo = GenerateMatricNo.GenerateMatricN(reg.Id + 1),
                        DepartmentId = register.DepartmentId

                    };


                    _context.AdmittedStudents.Add(adm);
                    _context.SaveChanges();

                }

                else
                {
                    var subject = "Admission!";
                    var body = $"We regret to inform you that your application for admission has been rejected by our admissions committee. As you have not met our Admission requirement. Your Total score was {percent}";
                    Email.SendEmail(register.Email, register.FirstName, subject, body);
                }





                return Ok(new
                {
                    message = "Registration successful ", // return this message
                    ApplicantId = response.ApplicantId,
                    Email = register.Email,
                    //Password= response.Password,
                    statusCode = 200

                });
            }
            return BadRequest(new
            {
                message = "Registration Unsuccessful"

            });
        }

        [HttpPost("ApplicantLogin")]
        public async Task<IActionResult> PostLogin([FromBody] UserLogin login)
        {
            var userExist = await _context.Registrations.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "Wrong Username or Password" });
            }
            else
            {
                return Ok(new
                {
                    message = "Login successful", // return this message
                    email = userExist.Email,
                    name = userExist.LastName,
                    statusCode = 200
                });
            }

        }

        [HttpPost("StudentLogin")]
        public async Task<IActionResult> PostStuLogin([FromBody] StudentLogin login)
        {
            var userExist = await _context.AdmittedStudents.Where(x => x.MatricNo == login.MatricNo && x.Password == login.Password).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "Wrong Username or Password" });
            }
            else
            {
                return Ok(new
                {
                    message = "Login successful", // return this message
                    email = userExist.MatricNo,
                    name = userExist.LastName,
                    statusCode = 200
                });
            }

        }



        [HttpPost("ChangePassword")]
        public async Task<IActionResult> PostChangepass([FromForm] changePassword model)
        {
            try
            {
                var userExist = await _context.AdmittedStudents.Where(x => x.MatricNo == model.MatricNo).FirstOrDefaultAsync();
                if (userExist == null)
                {
                    return BadRequest(new { message = "user does not exist" });
                }
                else if (model.NewPassword != model.ConfirmPassword)
                {
                    return BadRequest(new { message = "new password and confirmed password are not the same" });
                }
                if (userExist.Password == model.OldPassword)
                {
                    userExist.Password = model.NewPassword;
                    userExist.Password = model.OldPassword;
                    userExist.Password = model.ConfirmPassword;

                    _context.AdmittedStudents.Update(userExist);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        message = "password changed successful", // return this message
                        password = model.NewPassword,
                        statusCode = 200
                    });
                }
                else
                {
                    return Ok(new
                    {
                        message = "password does not match", // return this message                
                        statusCode = 404
                    });
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse
                {
                    StatusCode = 400,
                    Message = "An error occurred while processing the request.",
                    Data = null
                };

                return BadRequest(response);

            }
           


        }

        [HttpGet("GetAllDepartments")]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            try
            {
                if (_context.Departments == null)
                {
                    return NotFound();
                }
                var result = await _context.Departments.
                    Select(model => new DepartmentDTO
                    {
                        DepartmentId = model.Id.ToString(),
                        DepartmentName = model.DepartmentName,
                        FacultyId = model.FacultyId,

                    }).ToListAsync();

                var response = new ApiResponse
                {
                    StatusCode = 200,
                    Message = "aucess",
                    Data = result
                };

                return Ok(response);

            }
            catch (Exception ex)
            {

                var response = new ApiResponse
                {
                    StatusCode = 400,
                    Message = "An error occurred while processing the request.",
                    Data = null
                };

                return BadRequest(response);

            }
            }
        
           
            
           


        [HttpGet("GetDepartment{Id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDept(int Id)
        {
            //if (_context.Admins == null)
            //{
            //    return NotFound();
            //}
            var dept = await _context.Departments.Where(w => w.Id == Id).
                 Select(model => new DepartmentDTO
                 {
                     DepartmentId = model.Id.ToString(),
                     DepartmentName = model.DepartmentName,
                     FacultyId = model.FacultyId,

                 }).FirstOrDefaultAsync();



            if (dept == null)
            {
                return NotFound();
            }

            return dept;
        }


        [HttpPut("UpdateFaculty")]
        public async Task<IActionResult> PutFac(int FacultyId, [FromForm] FacultyDTO facultyDTO)
        {
            var facultyExist = await _context.Faculties.FindAsync(FacultyId);
            if (facultyExist == null)
            {
                return BadRequest(new { message = "Faculty does not exist" });
            }

            facultyExist.FacultyName = facultyDTO.FacultyName;
            //facultyExist.Name = facultyDTO.Name;
            //facultyExist.Email = facultyDTO.Email;

            _context.Faculties.Update(facultyExist);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                faculty = facultyDTO
            });
        }



        //[HttpGet("GetAllDepartments")]
        //public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        //{
        //    if (_context.Departments == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Departments.ToListAsync();
        //}

        [HttpGet("GetAllFaculties")]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            return await _context.Faculties.ToListAsync();
        }


        [HttpPut("UpdateAdmittedStudents")]
        public async Task<IActionResult> Putstu(string MatricNo, [FromForm] AdmittedStudent admin)
        {
            var userExist = await _context.AdmittedStudents.Where(x => x.MatricNo == MatricNo).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "user does not exist" });
            }
            userExist.ApplicantId = admin.ApplicantId;

            userExist.FirstName = admin.FirstName;
            userExist.MiddleName = admin.MiddleName;
            userExist.LastName = admin.LastName;
            userExist.PhoneNumber = admin.PhoneNumber;
            userExist.Email = admin.Email;
            userExist.Address = admin.Address;
            userExist.DOB = admin.DOB;
            userExist.Gender = admin.Gender;
            userExist.MaritalStatus = admin.MaritalStatus;
            userExist.Nationality = admin.Nationality;
            userExist.StateOfOrigin = admin.StateOfOrigin;
            userExist.Denomination = admin.Denomination;
            userExist.GuardianName = admin.GuardianName;
            userExist.GuardianPhoneNumber = admin.GuardianPhoneNumber;
            userExist.GuardianEmail = admin.GuardianEmail;
            userExist.GuardianAddress = admin.GuardianAddress;





            _context.AdmittedStudents.Update(userExist);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                register = admin,
            }
                );
        }



        [HttpPost("forgotpassword")]
         
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
        {
            var userExist = await _context.AdmittedStudents.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "user does not exist" });
            }

            else
            {
                var token = GenerateToken.Token(userExist.Id, userExist.Id);

                var adm = new TokenTable()
                {
                    Token = token,
                    Email=model.Email,
                    IsUsed=false,
                    CreatedDate= DateTime.UtcNow,
                };

                var subject = "Forgot Password!";
                var body = $" Dear {userExist.FirstName} {userExist.LastName}, kindly use this token to reset your Password. {token}   ";
                Email.SendEmail(userExist.Email, userExist.FirstName, subject, body);

                _context.TokenTables.Add(adm);
                await _context.SaveChangesAsync();

            }

            return Ok();
           
        }

        [HttpPost("Resetpassword")]

        public async Task<IActionResult> ResetPassword([FromForm] ResetPassword model)
        {           
               
            var userExist = await _context.AdmittedStudents.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "user does not exist" });
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new { message = "new password and confirmed password are not the same" });
            }
            if (string.IsNullOrEmpty(model.Token))
            {
                return BadRequest(new { message = "Invalid token" });
            }

            var userToken = _context.TokenTables.FirstOrDefault(x => x.Email == model.Email && x.Token == model.Token);
            if (userToken == null)
            {
                return BadRequest(new { message = "Invalid Token" });

            }
            else if (userToken != null && userToken.CreatedDate > DateTime.Now.AddMinutes(30) && userToken.IsUsed == false)
            {
                //&& x.CreatedDate <= DateTime.Now.AddMinutes(30) && x.IsUsed == false)
                return BadRequest(new { message = "Token expired" });
            }
            else if(userToken != null && userToken.IsUsed == true)
            {
                return BadRequest(new { message = "Token has been used" });
            }


            userExist.Password = model.NewPassword;
            _context.AdmittedStudents.Update(userExist);
            await _context.SaveChangesAsync();


            userToken.IsUsed = true;
            _context.TokenTables.Update(userToken);
            await _context.SaveChangesAsync();


            return Ok(new
                {
                    message = "password changed successful", // return this message
                    password = model.NewPassword,
                    statusCode = 200
                }
            

                
                
                );




        }
        }
    }
