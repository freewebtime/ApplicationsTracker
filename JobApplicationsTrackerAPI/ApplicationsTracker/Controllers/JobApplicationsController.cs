using ApplicationsTracker.Data;
using ApplicationsTracker.Dtos;
using ApplicationsTracker.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApplicationsTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public JobApplicationsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobApplicationRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                user = new User { 
                    Email = email
                };
                _db.Users.Add(user);
            }

            var application = new JobApplication
            {
                Company = request.Company,
                Position = request.Position,
                Notes = request.Notes,
                User = user,
                Status = request.Status
            };

            _db.JobApplications.Add(application);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> Get()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;

            var apps = await _db.JobApplications
                .Where(a => a.User.Email == email)
                .Select(a => new JobApplicationDto
                {
                    Id = a.Id,
                    Company = a.Company,
                    Position = a.Position,
                    AppliedAt = a.AppliedAt,
                    Status = a.Status
                })
                .ToListAsync();

            return Ok(apps);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus(Guid applicationId, ApplicationStatus status)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;

            var applicationById = await _db.JobApplications
                .FirstOrDefaultAsync(a => a.Id == applicationId);

            var application = await _db.JobApplications
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == applicationId && a.User.Email == email);
            if (application == null)
            {
                return NotFound();
            }
            application.Status = status;
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{applicationId}")]
        public async Task<IActionResult> Delete(Guid applicationId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return Unauthorized();

            var application = await _db.JobApplications
                .FirstOrDefaultAsync(a => a.Id == applicationId && a.UserId == user.Id);

            if (application == null)
                return NotFound();

            _db.JobApplications.Remove(application);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
