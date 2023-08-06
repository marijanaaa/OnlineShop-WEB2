namespace online_selling.Dto
{
    public class UpdateProfileDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? ChangedPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Picture { get; set; }
    }
}
