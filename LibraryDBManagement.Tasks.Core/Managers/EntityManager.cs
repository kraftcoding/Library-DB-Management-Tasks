using LibraryDBManagement.Tasks.Model;

namespace LibraryDBManagement.Tasks.Core.Managers
{
    public static class EntityManager
    {
        public static User GetUserObject(string name, string email, string password, string role)
        {
            var Usr = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Role = role
            };

            return Usr;
        }
    }
}
