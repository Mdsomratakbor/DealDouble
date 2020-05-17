using DealDouble.Database;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DealDouble.Services
{
    public class BidsServices
    {

        #region Singletone
        public static BidsServices Instance
        {
            get
            {
                if (instance == null) instance = new BidsServices();
                return instance;
            }
        }
        private static BidsServices instance { get; set; }
        #endregion


        public bool AddBid(Bid bid)
        {
            using (var context = new Context())
            {
                 context.Bids.Add(bid);
                return context.SaveChanges() > 0;
            }
        }
    }
}
