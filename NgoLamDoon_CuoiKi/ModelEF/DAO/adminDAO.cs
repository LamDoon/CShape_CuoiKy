using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
  public  class adminDAO
    {
        private NgoLamDoonContext db;

        public adminDAO()
        {
            db = new NgoLamDoonContext();
        }

        public List<UserAccount> ListAll()
        {
            return db.UserAccounts.ToList();
        }

        public int login(string UserName, string Password)
        {
            var result = db.UserAccounts.SingleOrDefault(x => x.UserName.Contains(UserName) && x.Password.Contains(Password) && x.Status.Contains("Active") );
            if (result == null)
            {
                return 0;
            }
            else
            {
              List<UserAccount>  model = db.UserAccounts.ToList();
                UserAccount users = db.UserAccounts.Where(val => val.UserName == UserName).Single<UserAccount>();
                if (users.ID.Contains("AD"))
                {
                    return 1;
                }
               else
                {
                    return 2;
                }         
                
            }
        }
    }
}
