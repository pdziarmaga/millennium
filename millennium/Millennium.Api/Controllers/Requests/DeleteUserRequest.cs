using System.ComponentModel.DataAnnotations;

namespace Millennium.Api.Controllers.Requests
{
    public class DeleteUserRequest
    {
        [Required]
        public long UserId { get; set; }
    }
}