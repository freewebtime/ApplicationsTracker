using ApplicationsTracker.Entities;

namespace ApplicationsTracker.Dtos
{
    public class CreateJobApplicationRequest
    {
        public string Company { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? Notes { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.None;
    }

    public class JobApplicationDto
    {
        public Guid Id { get; set; }
        public string Company { get; set; } = null!;
        public string Position { get; set; } = null!;
        public DateTime AppliedAt { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.None;
    }

    public class GuestLoginRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class GuestRegistrationRequestDto
    {
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class GuestLoginRegistrationResponseDto
    {
        public int Status { get; set; }

        public string Token { get; set; } = string.Empty;

        public string Error { get; set; } = string.Empty;
    }
}
