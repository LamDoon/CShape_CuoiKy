using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
   public class productDAO
    {
        private NgoLamDoonContext db;

        public productDAO()
        {
            db = new NgoLamDoonContext();
        }

        public IEnumerable<Product> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<Product> model = (from s in db.Products orderby s.Quantity ascending orderby s.UnitCost descending select s);
            if (!string.IsNullOrEmpty(keysearch))
                return model.Where(x => x.Name.Contains(keysearch)).ToPagedList(page, pagesize);
            return model.ToPagedList(page, pagesize);
        }
    }
}
