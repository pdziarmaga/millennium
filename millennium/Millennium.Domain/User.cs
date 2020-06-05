using Millennium.Shared.BaseObjects;

namespace Millennium.Domain
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public User(
            string name,
            string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }

        public void Update(
            string name,
            string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}