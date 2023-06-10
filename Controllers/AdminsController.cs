using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WebApplication3.models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
       // private readonly UserManager<ApplicantSignUp> _userManager;
        private readonly AdminContext _context;

        public AdminsController(AdminContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
          if (_context.Admins == null)
          {
              return NotFound();
          }
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
          //if (_context.Admins == null)
          //{
          //    return NotFound();
          //}
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> Putregister(string Email, [FromForm] register admin)
        {
            var userExist = await _context.Admins.Where(x => x.Email == Email).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "user does not exist" });
            }
            userExist.Password = admin.Password;
        
            userExist.Name = admin.Name;

            userExist.Email = admin.Email;   
           
            _context.Admins.Update(userExist);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                register = admin,
            }
                );
        }

        //POST: api/Admins
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddUser")]
        public async Task<IActionResult> Postregister([FromBody] register admin)
        {
            var adm = new Admin()
            {
                Email = admin.Email,
                Name = admin.Name,
                Password = admin.Password,
            };
            _context.Admins.Add(adm); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                return Ok(new
                {
                    message = "New Admin added successfully", // return this message
                    email = admin.Email,
                    statusCode = 200
                });
            }
            return BadRequest(new 
            {
                message = "failed to Add new Admin" 

            
            
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin([FromBody] Login login)
        {
            var userExist = await _context.Admins.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "Wrong Username or Password" });
            }
            else
            {
                return Ok(new
                {
                    message = "login successful", // return this message
                    email = userExist.Email,
                    statusCode = 200
                });
            }

        }
        

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> PostChangepasswordcs([FromForm]ChangePasswordcs model)
        {
            var userExist = await _context.Admins.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (userExist == null)
            {
                return BadRequest(new { message = "user does not exist" });
            }
            else if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new { message = "new password and confirmed password are not the same" });
            }
            if (userExist.Password == model.OldPassword )
            {
                userExist.Password = model.NewPassword;
                userExist.Password = model.OldPassword;
                userExist.Password = model.ConfirmPassword;

                _context.Admins.Update(userExist);
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
        //DELETE: api/Admins/5
        [HttpDelete("{id}")]    
        public async Task<IActionResult> DeleteAdmin(int? id)
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("DeleteStudent{MatricNo}")]
        public async Task<IActionResult> DeleteAdminApp(string? MatricNo)
        {
            if (_context.AdmittedStudents == null)
            {
                return NotFound();
            }
            var admin = await _context.AdmittedStudents.FindAsync(MatricNo);
            if (admin == null)
            {
                return NotFound();
            }

            _context.AdmittedStudents.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddFaculty")]
        public async Task<IActionResult> Postfaculty([FromForm] FacultyDTO admin)
        {
            var fac = new Faculty()
            {
                //FacultyId = admin.FacultyId,
                FacultyName = admin.FacultyName,
               
            };
            _context.Faculties.Add(fac); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                return Ok(new
                {
                    message = "New Faculty added successfully", // return this message
                    faculty = admin.FacultyName,
                    statusCode = 200
                });
            }
            return BadRequest(new
            {
                message = "failed to Add new Faculty"



            });
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> Postdepartment([FromForm] DepartmentDTO dept)
        {
            var dep = new Department()
            {
                //Id = dept.Id,
                DepartmentName = dept.DepartmentName,
                FacultyId = dept.FacultyId,
               

            };
            _context.Departments.Add(dep); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                return Ok(new
                {
                    message = "New Faculty added successfully", // return this message
                    department= dept.DepartmentName,
                    statusCode = 200
                });
            }
            return BadRequest(new
            {
                message = "failed to Add new Department"



            });
        }

        [HttpPost("AddCourses")]
        public async Task<IActionResult> Postcourse([FromForm] CourseDTO admin)
        {
            var cou = new Course
            {
                CourseCode = admin.CourseCode,
                CourseName = admin.CourseName,
                Credits = admin.Credits,
                DepartmentId = admin.DepartmentId,
                Semester=admin.Semester,
                Level=admin.Level,

            };
            _context.Courses.Add(cou); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                return Ok(new
                {
                    message = "New Course added successfully", // return this message
                    course = admin.CourseCode,
                    statusCode = 200
                });
            }
            return BadRequest(new
            {
                message = "failed to Add new Course"



            });
        }

        [HttpPost("AddHostel")]
        public async Task<IActionResult> PostHostel([FromForm] HostelDTO admin)
        {
            var hos = new Hostel()
            {
               
                HostelName = admin.HostelName,

            };
            _context.Hostels.Add(hos); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                return Ok(new
                {
                    message = "New Hostel added successfully", // return this message
                    hostel = admin.HostelName,
                    statusCode = 200
                });
            }
            return BadRequest(new
            {
                message = "failed to Add new Hostel"



            });
        }


        [HttpPost("AddWorshipCenter")]
        public async Task<IActionResult> PostWorshipCenter([FromForm] WorshipCenterDTO admin)
        {
            var hos = new WorshipCenter()
            {

                WorshipCenterName = admin.WorshipCenterName,

            };
            _context.WorshipCenters.Add(hos); // add what is coming from te api view
            int isSvaed = await _context.SaveChangesAsync(); // save
            if (isSvaed == 1) // check if it saved
            {
                return Ok(new
                {
                    message = "New Worship Center  added successfully", // return this message
                    WorshipCenter = admin.WorshipCenterName,
                    statusCode = 200
                });
            }
            return BadRequest(new
            {
                message = "Failed to Add new Worship Center"



            });
        }








        private bool AdminExists(int id)
        {
            return (_context.Admins?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}
