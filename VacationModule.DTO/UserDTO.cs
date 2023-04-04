namespace VacationModule.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = null!;

    }
}