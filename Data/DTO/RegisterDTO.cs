namespace Nettbutikk.Data.DTO
{
    public class RegisterDTO : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleCode { get; set; }
    }
}
