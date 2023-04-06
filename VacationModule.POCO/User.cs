namespace VacationModule.POCO
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public string Role { get; set; } = null!;

        //one-to-many relationship with VacationRequest
        public List<VacationRequest>? VacationRequests { get; set; }

    }
}