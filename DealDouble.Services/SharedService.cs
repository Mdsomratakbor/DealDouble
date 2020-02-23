using DealDouble.Database;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class SharedService
    {
        #region  Singleton
        public static SharedService Instance
        {
            get
            {
                if (instance == null) instance = new SharedService();
                return instance;
            }
        }
        private static SharedService instance { get; set; }
        #endregion
        public int SavePicture(Picture picture)
        {
            using (var contaxt = new Context())
            {
                contaxt.Pictures.Add(picture);
                contaxt.SaveChanges();
                return picture.ID;
            }
        }
    }
}
