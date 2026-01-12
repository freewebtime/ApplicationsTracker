using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationsTracker.Entities
{
    public class JobApplication
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Company { get; set; } = null!;

        [Required]
        public string Position { get; set; } = null!;

        public string? Notes { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.None;
    }

    public enum ApplicationStatus
    {
        None,
        Applied,
        Interviewing,
        Offered,
        Rejected,
        Accepted
    }
}
