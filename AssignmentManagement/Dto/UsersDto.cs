namespace AssignmentManagement.Dto
{
    public class UsersDto
    {
        public string Firstname { get; set; } = null!;

        public string? Lastname { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Phone { get; set; }

        public int Roleid { get; set; }

    }
}
