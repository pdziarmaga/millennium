using System.ComponentModel.DataAnnotations;

namespace Millennium.Api.Controllers.Requests
{
    public class GetUserRequest
    {
        [Required]
        public long UserId { get; set; }
    }
}