using System.ComponentModel.DataAnnotations;

namespace Millennium.Api.Controllers.Responses
{
    public class CreateUserResponse
    {
        [Required]
        public long UserId { get; set; }
    }
}