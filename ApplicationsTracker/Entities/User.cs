using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;

namespace ApplicationsTracker.Entities
{

    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
