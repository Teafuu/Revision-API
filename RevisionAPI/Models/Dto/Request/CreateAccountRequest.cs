namespace Revision_API.Models.Dto.Request
{
    public class CreateAccountRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
