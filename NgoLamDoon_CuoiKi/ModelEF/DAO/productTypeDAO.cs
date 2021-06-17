using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class productTypeDAO
    {
        private NgoLamDoonContext db;

        public productTypeDAO()
        {
            db = new NgoLamDoonContext();
        }

        public List<ProductType> ListAll()
        {
            IQueryable<ProductType> model = (from s in db.ProductTypes select s);          
            return model.ToList();
        }
    }
}
