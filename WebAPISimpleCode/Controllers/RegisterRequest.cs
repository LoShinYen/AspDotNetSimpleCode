namespace WebAPISimpleCode.Controllers
{
    public class RegisterRequest
    {
        public string Username { get; internal set; }
        public string Password { get; internal set; }
        public string Email { get; internal set; }
    }
}