using LibraryDBManagement.Tasks.Model;

namespace LibraryDBManagement.Tasks.Core
{
    public class DBManager
    {
        public void AddUser(User Usr)
        {
            using (var db = new LibraryDBContext())
            {               
                db.Users.Add(Usr);
                db.SaveChanges();
            }
        }
    }
}
