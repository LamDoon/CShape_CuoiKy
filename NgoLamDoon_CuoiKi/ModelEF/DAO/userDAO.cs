using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
   public class userDAO
    {
        private NgoLamDoonContext db;

        public userDAO()
        {
            db = new NgoLamDoonContext();
        }

        public IEnumerable<UserAccount> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<UserAccount> model = (from s in db.UserAccounts where s.ID.Contains("user") orderby s.Status ascending select s);
            if (!string.IsNullOrEmpty(keysearch))
                return model.Where(x => x.UserName.Contains(keysearch)).ToPagedList(page, pagesize);
            return model.ToPagedList(page,pagesize);
        }
    }
}
