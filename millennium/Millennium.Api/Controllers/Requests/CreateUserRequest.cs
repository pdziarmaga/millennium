using System.ComponentModel.DataAnnotations;

namespace Millennium.Api.Controllers.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Surname { get; set; }
    }
}