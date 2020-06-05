namespace Millennium.Application.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserDto(
            string name,
            string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}