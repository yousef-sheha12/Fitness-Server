namespace Fitness.Models.DTOs.Profile
{
    public class UpdatePasswordDto
    {
        public string? OldPassword { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
