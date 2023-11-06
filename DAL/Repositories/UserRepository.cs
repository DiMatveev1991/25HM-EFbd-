using dbEF.dbconfig;
using dbEF.BBL.model;
using Microsoft.EntityFrameworkCore;

namespace dbEF.DAL.Repositories
{
    internal class UserRepository
    {
        // 25.3.5   обновление имени пользователя(по Id)
        public static void UpdateUserNameById(dbconfig.AppContext db, int id, string name)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            user.Name = name;
            db.Users.Update(user);
            db.SaveChanges();
            ShowAvtor(db, id);
        }
        public static void ShowAvtor (dbconfig.AppContext db, int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            Console.WriteLine(user.Name);
        }
    }
}
