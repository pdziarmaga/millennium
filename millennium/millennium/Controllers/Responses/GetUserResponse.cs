namespace Millennium.Api.Controllers.Responses
{
    public class GetUserResponse
    {
        public UserModel User { get; set; }
    }

    public class UserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}